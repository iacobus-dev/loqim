using Loqim.Api.Dtos.Chat;

namespace Loqim.Api.Services;

public interface IChatOrchestratorService
{
    Task<ChatResponse> RespondAsync(ChatRequest request);
}
