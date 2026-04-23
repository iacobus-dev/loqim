namespace Loqim.Domain.Entities;
public class EscalationRule
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string TriggerType { get; set; } = string.Empty;
    public string TriggerCondition { get; set; } = string.Empty;
    public string MessageToUser { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}