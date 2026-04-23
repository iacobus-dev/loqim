namespace Loqim.Api.Dtos;

public class UpdateCatalogServiceItemRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool RequiresScheduling { get; set; }
    public string BookingLink { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
