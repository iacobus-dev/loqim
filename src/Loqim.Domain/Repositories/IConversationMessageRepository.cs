using Loqim.Domain.Entities;

namespace Loqim.Domain.Repositories;

public interface IConversationMessageRepository
{
    Task AddAsync(ConversationMessage message, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ConversationMessage>> GetByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ConversationMessage>> GetByTenantIdAndExternalUserIdAsync(
        Guid tenantId,
        string externalUserId,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ConversationMessage>> GetRecentByTenantIdAndExternalUserIdAsync(
        Guid tenantId,
        string externalUserId,
        int take,
        CancellationToken cancellationToken = default);
}
