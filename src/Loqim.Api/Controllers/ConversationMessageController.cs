using Loqim.Api.Dtos;
using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversationMessageController : ControllerBase
{
    private readonly IConversationMessageRepository _conversationMessageRepository;
    private readonly ITenantRepository _tenantRepository;

    public ConversationMessageController(
        IConversationMessageRepository conversationMessageRepository,
        ITenantRepository tenantRepository)
    {
        _conversationMessageRepository = conversationMessageRepository;
        _tenantRepository = tenantRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateConversationMessageRequest request)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
        if (tenant is null)
            return NotFound("Tenant not found.");

        if (string.IsNullOrWhiteSpace(request.ExternalUserId))
            return BadRequest("ExternalUserId is required.");

        if (string.IsNullOrWhiteSpace(request.Role))
            return BadRequest("Role is required.");

        if (string.IsNullOrWhiteSpace(request.Content))
            return BadRequest("Content is required.");

        var message = new ConversationMessage
        {
            TenantId = request.TenantId,
            ExternalUserId = request.ExternalUserId.Trim(),
            UserName = request.UserName.Trim(),
            Channel = string.IsNullOrWhiteSpace(request.Channel) ? "whatsapp" : request.Channel.Trim(),
            Role = request.Role.Trim(),
            Content = request.Content.Trim(),
            EscalatedToHuman = request.EscalatedToHuman,
            EscalationReason = request.EscalationReason.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _conversationMessageRepository.AddAsync(message);

        return Ok(message);
    }

    [HttpGet("tenant/{tenantId:guid}")]
    public async Task<IActionResult> GetByTenantId(Guid tenantId)
    {
        var messages = await _conversationMessageRepository.GetByTenantIdAsync(tenantId);

        return Ok(messages);
    }

    [HttpGet("by-user")]
    public async Task<IActionResult> GetConversationMessageByUser([FromQuery] GetConversationMessageByUserRequest request)
    {
        if (request.TenantId == Guid.Empty)
            return BadRequest("TenantId is required.");

        if (string.IsNullOrWhiteSpace(request.ExternalUserId))
            return BadRequest("ExternalUserId is required.");

        var messages = await _conversationMessageRepository.GetByTenantIdAndExternalUserIdAsync(
            request.TenantId,
            request.ExternalUserId.Trim());

        return Ok(messages);
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetRecent([FromQuery] GetRecentConversationMessagesRequest request)
    {
        if (request.TenantId == Guid.Empty)
            return BadRequest("TenantId is required.");

        if (string.IsNullOrWhiteSpace(request.ExternalUserId))
            return BadRequest("ExternalUserId is required.");

        if (request.Take <= 0)
            return BadRequest("Take must be greater than zero.");

        var messages = await _conversationMessageRepository.GetRecentByTenantIdAndExternalUserIdAsync(
            request.TenantId,
            request.ExternalUserId.Trim(),
            request.Take);

        return Ok(messages);
    }
}
