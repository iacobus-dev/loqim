using Loqim.Domain.Entities;

namespace Loqim.Domain.Repositories;

public interface ILeadCaptureRuleRepository
{
    Task AddAsync(LeadCaptureRule rule, CancellationToken cancellationToken = default);
    Task UpdateAsync(LeadCaptureRule rule, CancellationToken cancellationToken = default);
    Task<LeadCaptureRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LeadCaptureRule>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
    Task DeactivateAsync(LeadCaptureRule rule, CancellationToken cancellationToken = default);
}
