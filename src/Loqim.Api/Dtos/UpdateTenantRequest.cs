namespace Loqim.Api.Dtos;

public class UpdateTenantRequest
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
