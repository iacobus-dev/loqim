namespace Loqim.Api.Dtos;

public class CreateLeadCaptureRuleRequest
{
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TriggerContext { get; set; } = string.Empty;
    public bool AskName { get; set; }
    public bool AskPhone { get; set; }
    public bool AskEmail { get; set; }
    public bool AskPreferredDate { get; set; }
    public bool AskPreferredTime { get; set; }
    public bool AskProcedureOfInterest { get; set; }
}
