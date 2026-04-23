using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Loqim.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Infra.Repositories;

public class CatalogProductRepository : ICatalogProductRepository
{
    private readonly AppDbContext _context;

    public CatalogProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CatalogProduct product, CancellationToken cancellationToken = default)
    {
        _context.CatalogProducts.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CatalogProduct product, CancellationToken cancellationToken = default)
    {
        _context.CatalogProducts.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<CatalogProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.CatalogProducts
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CatalogProduct>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.CatalogProducts
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(CatalogProduct product, CancellationToken cancellationToken = default)
    {
        _context.CatalogProducts.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
