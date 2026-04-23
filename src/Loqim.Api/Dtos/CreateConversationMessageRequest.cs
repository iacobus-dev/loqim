namespace Loqim.Api.Dtos;

public class CreateConversationMessageRequest
{
    public Guid TenantId { get; set; }
    public string ExternalUserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Channel { get; set; } = "whatsapp";
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool EscalatedToHuman { get; set; }
    public string EscalationReason { get; set; } = string.Empty;
}
