namespace Loqim.Api.Dtos;

public class CreateCatalogServiceItemRequest
{
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool RequiresScheduling { get; set; }
    public string BookingLink { get; set; } = string.Empty;
}
