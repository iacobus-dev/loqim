namespace Loqim.Api.Dtos;

public class GetRecentConversationMessagesRequest
{
    public Guid TenantId { get; set; }
    public string ExternalUserId { get; set; } = string.Empty;
    public int Take { get; set; }
}
