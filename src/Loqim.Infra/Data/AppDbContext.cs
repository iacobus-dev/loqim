using Loqim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<BusinessProfile> BusinessProfiles => Set<BusinessProfile>();
    public DbSet<AiRule> AiRules => Set<AiRule>();
    public DbSet<LeadCaptureRule> LeadCaptureRules => Set<LeadCaptureRule>();
    public DbSet<EscalationRule> EscalationRules => Set<EscalationRule>();
    public DbSet<CatalogProduct> CatalogProducts => Set<CatalogProduct>();
    public DbSet<CatalogServiceItem> CatalogServiceItems => Set<CatalogServiceItem>();
    public DbSet<ConversationMessage> ConversationMessages => Set<ConversationMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
