using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Loqim.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Infra.Repositories;

public class ConversationMessageRepository : IConversationMessageRepository
{
    private readonly AppDbContext _context;

    public ConversationMessageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ConversationMessage message, CancellationToken cancellationToken = default)
    {
        _context.ConversationMessages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ConversationMessage>> GetByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.ConversationMessages
            .Where(x => x.TenantId == tenantId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ConversationMessage>> GetByTenantIdAndExternalUserIdAsync(
        Guid tenantId,
        string externalUserId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ConversationMessages
            .Where(x => x.TenantId == tenantId && x.ExternalUserId == externalUserId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ConversationMessage>> GetRecentByTenantIdAndExternalUserIdAsync(
        Guid tenantId,
        string externalUserId,
        int take,
        CancellationToken cancellationToken = default)
    {
        return await _context.ConversationMessages
            .Where(x => x.TenantId == tenantId && x.ExternalUserId == externalUserId)
            .OrderByDescending(x => x.CreatedAt)
            .Take(take)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
