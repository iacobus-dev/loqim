using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Loqim.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Infra.Repositories;

public class BusinessProfileRepository : IBusinessProfileRepository
{
    private readonly AppDbContext _context;

    public BusinessProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BusinessProfile profile, CancellationToken cancellationToken = default)
    {
        _context.BusinessProfiles.Add(profile);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<BusinessProfile?> GetByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        return _context.BusinessProfiles
            .FirstOrDefaultAsync(x => x.TenantId == tenantId, cancellationToken);
    }
}
