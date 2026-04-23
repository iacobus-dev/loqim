using Loqim.Domain.Entities;

namespace Loqim.Domain.Repositories;

public interface IAiRuleRepository
{
    Task AddAsync(AiRule rule, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AiRule>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
}
