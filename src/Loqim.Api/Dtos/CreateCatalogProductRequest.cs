namespace Loqim.Api.Dtos;

public class CreateCatalogProductRequest
{
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PurchaseLink { get; set; } = string.Empty;
}
