using Loqim.Domain.Enums;

namespace Loqim.Api.Dtos;

public class UpdateAiRuleRequest
{
    public AiRuleCategory Category { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Priority { get; set; }
    public bool IsActive { get; set; }
}
