using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Loqim.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Infra.Repositories;

public class LeadCaptureRuleRepository : ILeadCaptureRuleRepository
{
    private readonly AppDbContext _context;

    public LeadCaptureRuleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LeadCaptureRule rule, CancellationToken cancellationToken = default)
    {
        _context.LeadCaptureRules.Add(rule);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeactivateAsync(LeadCaptureRule rule, CancellationToken cancellationToken = default)
    {
        rule.IsActive = false;
        _context.LeadCaptureRules.Update(rule);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<LeadCaptureRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.LeadCaptureRules
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LeadCaptureRule>> GetActiveByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.LeadCaptureRules
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(LeadCaptureRule rule, CancellationToken cancellationToken = default)
    {
        _context.LeadCaptureRules.Update(rule);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
