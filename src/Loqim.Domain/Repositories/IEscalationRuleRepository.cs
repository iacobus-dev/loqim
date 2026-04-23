using Loqim.Domain.Entities;

namespace Loqim.Domain.Repositories;

public interface IEscalationRuleRepository
{
    Task AddAsync(EscalationRule rule, CancellationToken cancellationToken = default);
    Task UpdateAsync(EscalationRule rule, CancellationToken cancellationToken = default);
    Task<EscalationRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EscalationRule>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
    Task DeactivateAsync(EscalationRule rule, CancellationToken cancellationToken = default);
}
