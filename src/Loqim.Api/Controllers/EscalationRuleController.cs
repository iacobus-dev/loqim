using Loqim.Api.Dtos;
using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EscalationRuleController : ControllerBase
{
    private readonly IEscalationRuleRepository _escalationRuleRepository;
    private readonly ITenantRepository _tenantRepository;

    public EscalationRuleController(
        IEscalationRuleRepository escalationRuleRepository,
        ITenantRepository tenantRepository)
    {
        _escalationRuleRepository = escalationRuleRepository;
        _tenantRepository = tenantRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEscalationRuleRequest request)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
        if (tenant is null)
            return NotFound("Tenant not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.TriggerType))
            return BadRequest("TriggerType is required.");

        if (string.IsNullOrWhiteSpace(request.TriggerCondition))
            return BadRequest("TriggerCondition is required.");

        if (string.IsNullOrWhiteSpace(request.MessageToUser))
            return BadRequest("MessageToUser is required.");

        var rule = new EscalationRule
        {
            TenantId = request.TenantId,
            Name = request.Name.Trim(),
            TriggerType = request.TriggerType.Trim(),
            TriggerCondition = request.TriggerCondition.Trim(),
            MessageToUser = request.MessageToUser.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _escalationRuleRepository.AddAsync(rule);

        return Ok(rule);
    }

    [HttpGet("tenant/{tenantId:guid}")]
    public async Task<IActionResult> GetByTenantId(Guid tenantId)
    {
        var rules = await _escalationRuleRepository.GetActiveByTenantIdAsync(tenantId);

        return Ok(rules);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEscalationRuleRequest request)
    {
        var rule = await _escalationRuleRepository.GetByIdAsync(id);
        if (rule is null)
            return NotFound("Escalation rule not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.TriggerType))
            return BadRequest("TriggerType is required.");

        if (string.IsNullOrWhiteSpace(request.TriggerCondition))
            return BadRequest("TriggerCondition is required.");

        if (string.IsNullOrWhiteSpace(request.MessageToUser))
            return BadRequest("MessageToUser is required.");

        rule.Name = request.Name.Trim();
        rule.TriggerType = request.TriggerType.Trim();
        rule.TriggerCondition = request.TriggerCondition.Trim();
        rule.MessageToUser = request.MessageToUser.Trim();
        rule.IsActive = request.IsActive;

        await _escalationRuleRepository.UpdateAsync(rule);

        return Ok(rule);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var rule = await _escalationRuleRepository.GetByIdAsync(id);
        if (rule is null)
            return NotFound("Escalation rule not found.");

        await _escalationRuleRepository.DeactivateAsync(rule);

        return NoContent();
    }
}
