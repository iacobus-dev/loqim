using Loqim.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromptController : ControllerBase
{
    private readonly PromptBuilderService _promptBuilderService;

    public PromptController(PromptBuilderService promptBuilderService)
    {
        _promptBuilderService = promptBuilderService;
    }

    [HttpGet("{tenantId:guid}")]
    public async Task<IActionResult> GetPrompt(Guid tenantId)
    {
        var prompt = await _promptBuilderService.BuildTenantPromptAsync(tenantId);

        if (prompt is null)
            return NotFound("Tenant not found.");

        return Ok(new { prompt });
    }
}