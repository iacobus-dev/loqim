using Loqim.Domain.Entities;

namespace Loqim.Domain.Repositories;

public interface ICatalogServiceItemRepository
{
    Task AddAsync(CatalogServiceItem serviceItem, CancellationToken cancellationToken = default);
    Task<CatalogServiceItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CatalogServiceItem>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogServiceItem serviceItem, CancellationToken cancellationToken = default);
    Task DeleteAsync(CatalogServiceItem serviceItem, CancellationToken cancellationToken = default);
}
