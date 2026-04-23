using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Loqim.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Infra.Repositories;

public class EscalationRuleRepository : IEscalationRuleRepository
{
    private readonly AppDbContext _context;

    public EscalationRuleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(EscalationRule rule, CancellationToken cancellationToken = default)
    {
        _context.EscalationRules.Add(rule);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeactivateAsync(EscalationRule rule, CancellationToken cancellationToken = default)
    {
        rule.IsActive = false;
        _context.EscalationRules.Update(rule);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<EscalationRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.EscalationRules
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<EscalationRule>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.EscalationRules
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(EscalationRule rule, CancellationToken cancellationToken = default)
    {
        _context.EscalationRules.Update(rule);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
