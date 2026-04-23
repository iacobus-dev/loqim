using Loqim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loqim.Infra.Mappings;

public class ConversationMessageConfiguration : IEntityTypeConfiguration<ConversationMessage>
{
    public void Configure(EntityTypeBuilder<ConversationMessage> builder)
    {
        builder.ToTable("conversation_messages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalUserId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.UserName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Channel)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Role)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(x => x.EscalatedToHuman)
            .IsRequired();

        builder.Property(x => x.EscalationReason)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.TenantId);
        builder.HasIndex(x => new { x.TenantId, x.ExternalUserId, x.CreatedAt });
    }
}
