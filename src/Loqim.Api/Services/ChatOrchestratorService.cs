using Loqim.Api.Dtos.Chat;
using Loqim.Domain.Entities;
using Loqim.Infra.Data;
using Microsoft.EntityFrameworkCore;
namespace Loqim.Api.Services;

public class ChatOrchestratorService : IChatOrchestratorService
{
    private readonly AppDbContext _context;
    private readonly IPromptBuilderService _promptBuilderService;
    private readonly IOpenAiService _openAiService;

    public ChatOrchestratorService(
        AppDbContext context,
        IPromptBuilderService promptBuilderService,
        IOpenAiService openAiService)
    {
        _context = context;
        _promptBuilderService = promptBuilderService;
        _openAiService = openAiService;
    }

    public async Task<ChatResponse> RespondAsync(ChatRequest request)
    {
        // 1. salva mensagem do usuário
        var userMessage = new ConversationMessage
        {
            TenantId = request.TenantId,
            ExternalUserId = request.ExternalUserId,
            UserName = request.UserName,
            Channel = "whatsapp",
            Role = "user",
            Content = request.Message,
            CreatedAt = DateTime.UtcNow
        };

        await _context.ConversationMessages.AddAsync(userMessage);
        await _context.SaveChangesAsync();

        // 2. carrega BusinessProfile
        var profile = await _context.BusinessProfiles
            .FirstOrDefaultAsync(x => x.TenantId == request.TenantId);

        // 3. carrega AiRules
        var aiRules = await _context.AiRules
            .Where(x => x.TenantId == request.TenantId && x.IsActive)
            .OrderByDescending(x => x.Priority)
            .ThenBy(x => x.CreatedAt)
            .ToListAsync();

        // 4. carrega CatalogServiceItem
        var catalogServices = await _context.CatalogServiceItems
            .Where(x => x.TenantId == request.TenantId && x.IsActive)
            .ToListAsync();

        // 5. carrega CatalogProduct
        var catalogProducts = await _context.CatalogProducts
            .Where(x => x.TenantId == request.TenantId && x.IsActive)
            .ToListAsync();

        // 6. carrega LeadCaptureRule
        var leadCaptureRules = await _context.LeadCaptureRules
            .Where(x => x.TenantId == request.TenantId && x.IsActive)
            .ToListAsync();

        // 7. carrega EscalationRule
        var escalationRules = await _context.EscalationRules
            .Where(x => x.TenantId == request.TenantId && x.IsActive)
            .ToListAsync();

        // 8. carrega últimas mensagens da conversa
        var recentMessages = await _context.ConversationMessages
            .Where(x => x.TenantId == request.TenantId
                     && x.ExternalUserId == request.ExternalUserId)
            .OrderByDescending(x => x.CreatedAt)
            .Take(20)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        // opcional: verificar escalonamento antes da OpenAI
        var escalation = CheckEscalation(request.Message, escalationRules);
        if (escalation.ShouldEscalate)
        {
            var escalationReply = escalation.MessageToUser;

            var assistantEscalationMessage = new ConversationMessage
            {
                TenantId = request.TenantId,
                ExternalUserId = request.ExternalUserId,
                UserName = request.UserName,
                Channel = "whatsapp",
                Role = "assistant",
                Content = escalationReply,
                EscalatedToHuman = true,
                EscalationReason = escalation.Reason,
                CreatedAt = DateTime.UtcNow
            };

            await _context.ConversationMessages.AddAsync(assistantEscalationMessage);
            await _context.SaveChangesAsync();

            return new ChatResponse
            {
                Reply = escalationReply,
                EscalatedToHuman = true,
                EscalationReason = escalation.Reason
            };
        }

        // 9. monta prompt
        var systemPrompt = await _promptBuilderService.BuildTenantPromptAsync(
            request.TenantId,
            profile,
            aiRules,
            catalogServices,
            catalogProducts,
            leadCaptureRules,
            escalationRules,
            recentMessages);

        // 10. chama OpenAI
        var openAiReply = await _openAiService.GenerateReplyAsync(
            systemPrompt,
            recentMessages,
            request.Message);

        // 11. salva resposta da IA
        var assistantMessage = new ConversationMessage
        {
            TenantId = request.TenantId,
            ExternalUserId = request.ExternalUserId,
            UserName = request.UserName,
            Channel = "whatsapp",
            Role = "assistant",
            Content = openAiReply,
            EscalatedToHuman = false,
            EscalationReason = string.Empty,
            CreatedAt = DateTime.UtcNow
        };

        await _context.ConversationMessages.AddAsync(assistantMessage);
        await _context.SaveChangesAsync();

        // 12. retorna resposta
        return new ChatResponse
        {
            Reply = openAiReply,
            EscalatedToHuman = false,
            EscalationReason = string.Empty
        };
    }

    private static EscalationCheckResult CheckEscalation(
        string userMessage,
        List<EscalationRule> escalationRules)
    {
        var normalizedMessage = userMessage.ToLowerInvariant();

        foreach (var rule in escalationRules)
        {
            var conditions = rule.TriggerCondition
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var condition in conditions)
            {
                if (normalizedMessage.Contains(condition.ToLowerInvariant()))
                {
                    return new EscalationCheckResult
                    {
                        ShouldEscalate = true,
                        Reason = rule.Name,
                        MessageToUser = string.IsNullOrWhiteSpace(rule.MessageToUser)
                            ? "Vou encaminhar seu atendimento para nossa equipe humana."
                            : rule.MessageToUser
                    };
                }
            }
        }

        return new EscalationCheckResult
        {
            ShouldEscalate = false
        };
    }
}
