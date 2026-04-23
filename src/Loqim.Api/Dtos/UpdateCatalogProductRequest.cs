namespace Loqim.Api.Dtos;

public class UpdateCatalogProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PurchaseLink { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
