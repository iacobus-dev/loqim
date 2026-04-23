using Loqim.Domain.Entities;
using Loqim.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Loqim.Api.Services;

public class PromptBuilderService : IPromptBuilderService
{
    private readonly AppDbContext _context;

    public PromptBuilderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> BuildTenantPromptAsync(Guid tenantId)
    {
        var profile = await _context.BusinessProfiles
            .FirstOrDefaultAsync(x => x.TenantId == tenantId);

        var aiRules = await _context.AiRules
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .OrderByDescending(x => x.Priority)
            .ThenBy(x => x.CreatedAt)
            .ToListAsync();

        var catalogServices = await _context.CatalogServiceItems
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .ToListAsync();

        var catalogProducts = await _context.CatalogProducts
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .ToListAsync();

        var leadCaptureRules = await _context.LeadCaptureRules
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .ToListAsync();

        var escalationRules = await _context.EscalationRules
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .ToListAsync();

        var recentMessages = await _context.ConversationMessages
            .Where(x => x.TenantId == tenantId)
            .OrderByDescending(x => x.CreatedAt)
            .Take(10)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        return await BuildTenantPromptAsync(
            tenantId,
            profile,
            aiRules,
            catalogServices,
            catalogProducts,
            leadCaptureRules,
            escalationRules,
            recentMessages);
    }

    public async Task<string> BuildTenantPromptAsync(
        Guid tenantId,
        BusinessProfile? profile,
        List<AiRule> aiRules,
        List<CatalogServiceItem> catalogServices,
        List<CatalogProduct> catalogProducts,
        List<LeadCaptureRule> leadCaptureRules,
        List<EscalationRule> escalationRules,
        List<ConversationMessage> recentMessages)
    {
        var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);

        if (tenant is null)
            return "Você é um assistente virtual útil e educado.";

        var sb = new StringBuilder();

        sb.AppendLine($"Você é o assistente virtual da empresa {tenant.Name}.");
        sb.AppendLine("Responda em português do Brasil, de forma natural, clara e útil.");
        sb.AppendLine();

        if (profile is not null)
        {
            sb.AppendLine("Contexto do negócio:");
            sb.AppendLine($"- Segmento: {profile.Segment}");
            sb.AppendLine($"- Descrição: {profile.Description}");
            sb.AppendLine($"- Tom de voz: {profile.ToneOfVoice}");
            sb.AppendLine($"- Objetivo principal: {profile.MainGoal}");
            sb.AppendLine($"- Horário de atendimento: {profile.BusinessHours}");
            sb.AppendLine();
        }

        if (catalogServices.Any())
        {
            sb.AppendLine("Serviços oferecidos:");
            foreach (var service in catalogServices)
            {
                sb.AppendLine($"- {service.Name}: {service.Description}");
                if (service.RequiresScheduling && !string.IsNullOrWhiteSpace(service.BookingLink))
                    sb.AppendLine($"  Link de agendamento: {service.BookingLink}");
            }
            sb.AppendLine();
        }

        if (catalogProducts.Any())
        {
            sb.AppendLine("Produtos oferecidos:");
            foreach (var product in catalogProducts)
            {
                sb.AppendLine($"- {product.Name}: {product.Description}");
                if (!string.IsNullOrWhiteSpace(product.PurchaseLink))
                    sb.AppendLine($"  Link de compra: {product.PurchaseLink}");
            }
            sb.AppendLine();
        }

        if (aiRules.Any())
        {
            sb.AppendLine("Regras que você deve seguir:");
            foreach (var rule in aiRules)
            {
                sb.AppendLine($"- [{rule.Category}] {rule.Name}: {rule.Content}");
            }
            sb.AppendLine();
        }

        if (leadCaptureRules.Any())
        {
            sb.AppendLine("Regras de captura de lead:");
            foreach (var rule in leadCaptureRules)
            {
                sb.AppendLine($"- {rule.Name}: {rule.TriggerContext}");
                var fields = new List<string>();

                if (rule.AskName) fields.Add("nome");
                if (rule.AskPhone) fields.Add("telefone");
                if (rule.AskEmail) fields.Add("email");
                if (rule.AskPreferredDate) fields.Add("data preferida");
                if (rule.AskPreferredTime) fields.Add("horário preferido");
                if (rule.AskProcedureOfInterest) fields.Add("procedimento de interesse");

                if (fields.Any())
                    sb.AppendLine($"  Quando fizer sentido, tente captar: {string.Join(", ", fields)}.");
            }
            sb.AppendLine();
        }

        if (escalationRules.Any())
        {
            sb.AppendLine("Regras de escalonamento:");
            foreach (var rule in escalationRules)
            {
                sb.AppendLine($"- {rule.Name}: {rule.TriggerCondition}");
            }
            sb.AppendLine();
        }

        sb.AppendLine("Instruções gerais:");
        sb.AppendLine("- Nunca invente preços, políticas, prazos ou informações clínicas.");
        sb.AppendLine("- Não faça diagnóstico nem prescrição.");
        sb.AppendLine("- Se faltar informação, diga que vai verificar.");
        sb.AppendLine("- Sempre conduza para o próximo passo útil, quando fizer sentido.");
        sb.AppendLine("- Se o cliente quiser agendar, ajude a direcionar para o link ou para o atendimento humano.");
        sb.AppendLine();

        if (recentMessages.Any())
        {
            sb.AppendLine("Resumo das últimas mensagens da conversa:");
            foreach (var msg in recentMessages.TakeLast(10))
            {
                sb.AppendLine($"- {msg.Role}: {msg.Content}");
            }
        }

        return sb.ToString();
    }
}
