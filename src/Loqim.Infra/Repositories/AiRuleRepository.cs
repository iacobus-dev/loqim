using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Loqim.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Infra.Repositories;

public class AiRuleRepository : IAiRuleRepository
{
    private readonly AppDbContext _context;

    public AiRuleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AiRule rule, CancellationToken cancellationToken = default)
    {
        _context.AiRules.Add(rule);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<AiRule>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.AiRules
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
