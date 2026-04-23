namespace Loqim.Domain.Entities;
public class ConversationMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    public string ExternalUserId { get; set; } = string.Empty; // telefone whatsapp
    public string UserName { get; set; } = string.Empty;

    public string Channel { get; set; } = "whatsapp";
    public string Role { get; set; } = string.Empty; // user | assistant | system
    public string Content { get; set; } = string.Empty;

    public bool EscalatedToHuman { get; set; }
    public string EscalationReason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}