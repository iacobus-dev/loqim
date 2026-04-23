using Loqim.Api.Dtos.Chat;
using Loqim.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IChatOrchestratorService _chatOrchestratorService;

    public ChatController(IChatOrchestratorService chatOrchestratorService)
    {
        _chatOrchestratorService = chatOrchestratorService;
    }

    [HttpPost("respond")]
    public async Task<ActionResult<ChatResponse>> Respond([FromBody] ChatRequest request)
    {
        var response = await _chatOrchestratorService.RespondAsync(request);

        return Ok(response);
    }
}
