using Loqim.Domain.Entities;

namespace Loqim.Domain.Repositories;

public interface IBusinessProfileRepository
{
    Task AddAsync(BusinessProfile profile, CancellationToken cancellationToken = default);
    Task<BusinessProfile?> GetByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
}
