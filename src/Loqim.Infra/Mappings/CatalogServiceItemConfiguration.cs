using Loqim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loqim.Infra.Mappings;

public class CatalogServiceItemConfiguration : IEntityTypeConfiguration<CatalogServiceItem>
{
    public void Configure(EntityTypeBuilder<CatalogServiceItem> builder)
    {
        builder.ToTable("catalog_service_items");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.RequiresScheduling)
            .IsRequired();

        builder.Property(x => x.BookingLink)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.TenantId);
    }
}
