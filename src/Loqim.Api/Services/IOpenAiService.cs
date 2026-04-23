using Loqim.Domain.Entities;

namespace Loqim.Api.Services;

public interface IOpenAiService
{
    Task<string> GenerateReplyAsync(
        string systemPrompt,
        List<ConversationMessage> recentMessages,
        string currentUserMessage);
}
