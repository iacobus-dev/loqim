namespace Loqim.Domain.Entities;
public class LeadCaptureRule
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string TriggerContext { get; set; } = string.Empty;
    public bool AskName { get; set; }
    public bool AskPhone { get; set; }
    public bool AskEmail { get; set; }
    public bool AskPreferredDate { get; set; }
    public bool AskPreferredTime { get; set; }
    public bool AskProcedureOfInterest { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}