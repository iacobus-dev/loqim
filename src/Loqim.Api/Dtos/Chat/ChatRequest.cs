namespace Loqim.Api.Dtos.Chat;

public class ChatRequest
{
    public Guid TenantId { get; set; }
    public string ExternalUserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
