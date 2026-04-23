using Loqim.Api.Dtos;
using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiRulesController : ControllerBase
{
    private readonly IAiRuleRepository _aiRuleRepository;
    private readonly ITenantRepository _tenantRepository;

    public AiRulesController(
        IAiRuleRepository aiRuleRepository,
        ITenantRepository tenantRepository)
    {
        _aiRuleRepository = aiRuleRepository;
        _tenantRepository = tenantRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAiRuleRequest request)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
        if (tenant is null)
            return NotFound("Tenant not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Content))
            return BadRequest("Content is required.");

        var rule = new AiRule
        {
            TenantId = request.TenantId,
            Category = request.Category,
            Name = request.Name.Trim(),
            Content = request.Content.Trim(),
            Priority = request.Priority,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _aiRuleRepository.AddAsync(rule);

        return Ok(rule);
    }

    [HttpGet("tenant/{tenantId:guid}")]
    public async Task<IActionResult> GetByTenant(Guid tenantId)
    {
        var rules = await _aiRuleRepository.GetActiveByTenantIdAsync(tenantId);

        return Ok(rules);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAiRuleRequest request)
    {
        var rule = await _aiRuleRepository.GetByIdAsync(id);
        if (rule is null)
            return NotFound("AI rule not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Content))
            return BadRequest("Content is required.");

        rule.Category = request.Category;
        rule.Name = request.Name.Trim();
        rule.Content = request.Content.Trim();
        rule.Priority = request.Priority;
        rule.IsActive = request.IsActive;

        await _aiRuleRepository.UpdateAsync(rule);

        return Ok(rule);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var rule = await _aiRuleRepository.GetByIdAsync(id);
        if (rule is null)
            return NotFound("AI rule not found.");

        await _aiRuleRepository.DeactivateAsync(rule);

        return NoContent();
    }
}
