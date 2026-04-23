using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Loqim.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Infra.Repositories;

public class CatalogServiceItemRepository : ICatalogServiceItemRepository
{
    private readonly AppDbContext _context;

    public CatalogServiceItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CatalogServiceItem serviceItem, CancellationToken cancellationToken = default)
    {
        _context.CatalogServiceItems.Add(serviceItem);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<CatalogServiceItem>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.CatalogServiceItems
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}
