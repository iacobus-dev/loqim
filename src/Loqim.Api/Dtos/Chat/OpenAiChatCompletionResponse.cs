namespace Loqim.Api.Dtos.Chat;

public class OpenAiChatCompletionResponse
{
    public List<OpenAiChoice> Choices { get; set; } = new();
}

public class OpenAiChoice
{
    public OpenAiMessage Message { get; set; } = new();
}

public class OpenAiMessage
{
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
