namespace Loqim.Api.Dtos;

public class GetConversationMessageByUserRequest
{
    public Guid TenantId { get; set; }
    public string ExternalUserId { get; set; } = string.Empty;
}
