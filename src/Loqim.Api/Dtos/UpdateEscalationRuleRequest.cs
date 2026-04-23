namespace Loqim.Api.Dtos;

public class UpdateEscalationRuleRequest
{
    public string Name { get; set; } = string.Empty;
    public string TriggerType { get; set; } = string.Empty;
    public string TriggerCondition { get; set; } = string.Empty;
    public string MessageToUser { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
