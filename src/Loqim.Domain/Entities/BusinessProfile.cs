namespace Loqim.Domain.Entities;

public class BusinessProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    public string Segment { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ToneOfVoice { get; set; } = string.Empty;
    public string MainGoal { get; set; } = string.Empty;
    public string BusinessHours { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
