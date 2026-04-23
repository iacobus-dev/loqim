using Loqim.Domain.Entities;

namespace Loqim.Domain.Repositories;

public interface ICatalogServiceItemRepository
{
    Task AddAsync(CatalogServiceItem serviceItem, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CatalogServiceItem>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
}
