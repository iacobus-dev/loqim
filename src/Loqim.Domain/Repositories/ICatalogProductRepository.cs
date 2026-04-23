using Loqim.Domain.Entities;

namespace Loqim.Domain.Repositories;

public interface ICatalogProductRepository
{
    Task AddAsync(CatalogProduct product, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CatalogProduct>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
}
