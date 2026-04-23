namespace Loqim.Api.Dtos.Chat;

public class EscalationCheckResult
{
    public bool ShouldEscalate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string MessageToUser { get; set; } = string.Empty;
}
