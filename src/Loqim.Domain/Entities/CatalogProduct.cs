namespace Loqim.Domain.Entities;

public class CatalogProduct
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PurchaseLink { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
