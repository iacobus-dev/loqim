using Loqim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loqim.Infra.Mappings;

public class LeadCaptureRuleConfiguration : IEntityTypeConfiguration<LeadCaptureRule>
{
    public void Configure(EntityTypeBuilder<LeadCaptureRule> builder)
    {
        builder.ToTable("lead_capture_rules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.TriggerContext)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.TenantId);
    }
}
