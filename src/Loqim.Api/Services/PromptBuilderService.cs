using Loqim.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Loqim.Api.Services;

public class PromptBuilderService
{
    private readonly AppDbContext _context;

    public PromptBuilderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string?> BuildTenantPromptAsync(Guid tenantId)
    {
        var tenant = await _context.Tenants
            .FirstOrDefaultAsync(x => x.Id == tenantId);

        if (tenant is null)
            return null;

        var profile = await _context.BusinessProfiles
            .FirstOrDefaultAsync(x => x.TenantId == tenantId);

        var rules = await _context.AiRules
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        var sb = new StringBuilder();

        sb.AppendLine($"Você é o assistente virtual da empresa {tenant.Name}.");
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

        if (rules.Any())
        {
            sb.AppendLine("Regras que você deve seguir:");
            foreach (var rule in rules)
            {
                sb.AppendLine($"- {rule.Name}: {rule.Content}");
            }
            sb.AppendLine();
        }

        sb.AppendLine("Instruções gerais:");
        sb.AppendLine("- Responda de forma clara, natural e útil.");
        sb.AppendLine("- Nunca invente preços, políticas ou prazos.");
        sb.AppendLine("- Se faltar informação, diga que vai verificar.");
        sb.AppendLine("- Sempre conduza para o próximo passo útil, quando fizer sentido.");
        sb.AppendLine("- Se o assunto estiver fora do escopo da empresa, informe isso educadamente.");

        return sb.ToString();
    }
}