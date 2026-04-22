namespace Loqim.Api.Dtos;

public class CreateBusinessProfileRequest
{
    public Guid TenantId { get; set; }
    public string Segment { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ToneOfVoice { get; set; } = string.Empty;
    public string MainGoal { get; set; } = string.Empty;
    public string BusinessHours { get; set; } = string.Empty;
}