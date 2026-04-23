namespace Loqim.Api.Dtos.Chat;

public class ChatResponse
{
    public string Reply { get; set; } = string.Empty;
    public bool EscalatedToHuman { get; set; }
    public string EscalationReason { get; set; } = string.Empty;
}
