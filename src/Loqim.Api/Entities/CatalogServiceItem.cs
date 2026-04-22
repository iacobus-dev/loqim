namespace Loqim.Api.Entities;

public class CatalogServiceItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool RequiresScheduling { get; set; }
    public string BookingLink { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
