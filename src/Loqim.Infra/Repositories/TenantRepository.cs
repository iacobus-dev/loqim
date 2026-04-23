using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Loqim.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Infra.Repositories;

public class TenantRepository : ITenantRepository
{
    private readonly AppDbContext _context;

    public TenantRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Tenant tenant, CancellationToken cancellationToken = default)
    {
        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Tenant tenant, CancellationToken cancellationToken = default)
    {
        _context.Tenants.Update(tenant);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Tenants.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Tenant>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Tenants
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Tenants
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<bool> SlugExistsAsync(string slug, Guid? excludeTenantId = null, CancellationToken cancellationToken = default)
    {
        return _context.Tenants.AnyAsync(
            x => x.Slug == slug && (!excludeTenantId.HasValue || x.Id != excludeTenantId.Value),
            cancellationToken);
    }
}
