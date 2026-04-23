using Loqim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loqim.Infra.Mappings;

public class EscalationRuleConfiguration : IEntityTypeConfiguration<EscalationRule>
{
    public void Configure(EntityTypeBuilder<EscalationRule> builder)
    {
        builder.ToTable("escalation_rules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.TriggerType)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.TriggerCondition)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.MessageToUser)
            .HasMaxLength(2000)
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
