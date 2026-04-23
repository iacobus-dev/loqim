using Loqim.Domain.Enums;

namespace Loqim.Api.Dtos;

public class CreateAiRuleRequest
{
    public Guid TenantId { get; set; }
    public AiRuleCategory Category { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Priority { get; set; } = 0;
}
