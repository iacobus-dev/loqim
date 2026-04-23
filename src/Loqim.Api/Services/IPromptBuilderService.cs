using Loqim.Domain.Entities;

namespace Loqim.Api.Services;

public interface IPromptBuilderService
{
    Task<string> BuildTenantPromptAsync(Guid tenantId);
    Task<string> BuildTenantPromptAsync(
        Guid tenantId,
        BusinessProfile? profile,
        List<AiRule> aiRules,
        List<CatalogServiceItem> catalogServices,
        List<CatalogProduct> catalogProducts,
        List<LeadCaptureRule> leadCaptureRules,
        List<EscalationRule> escalationRules,
        List<ConversationMessage> recentMessages);
}
