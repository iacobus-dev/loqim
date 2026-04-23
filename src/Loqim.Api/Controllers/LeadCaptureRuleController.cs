using Loqim.Api.Dtos;
using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadCaptureRuleController : ControllerBase
{
    private readonly ILeadCaptureRuleRepository _leadCaptureRuleRepository;
    private readonly ITenantRepository _tenantRepository;

    public LeadCaptureRuleController(
        ILeadCaptureRuleRepository leadCaptureRuleRepository,
        ITenantRepository tenantRepository)
    {
        _leadCaptureRuleRepository = leadCaptureRuleRepository;
        _tenantRepository = tenantRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeadCaptureRuleRequest request)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
        if (tenant is null)
            return NotFound("Tenant not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.TriggerContext))
            return BadRequest("TriggerContext is required.");

        var rule = new LeadCaptureRule
        {
            TenantId = request.TenantId,
            Name = request.Name.Trim(),
            TriggerContext = request.TriggerContext.Trim(),
            AskName = request.AskName,
            AskPhone = request.AskPhone,
            AskEmail = request.AskEmail,
            AskPreferredDate = request.AskPreferredDate,
            AskPreferredTime = request.AskPreferredTime,
            AskProcedureOfInterest = request.AskProcedureOfInterest,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _leadCaptureRuleRepository.AddAsync(rule);

        return Ok(rule);
    }

    [HttpGet("tenant/{tenantId:guid}")]
    public async Task<IActionResult> GetByTenantId(Guid tenantId)
    {
        var rules = await _leadCaptureRuleRepository.GetActiveByTenantIdAsync(tenantId);

        return Ok(rules);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLeadCaptureRuleRequest request)
    {
        var rule = await _leadCaptureRuleRepository.GetByIdAsync(id);
        if (rule is null)
            return NotFound("Lead capture rule not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.TriggerContext))
            return BadRequest("TriggerContext is required.");

        rule.Name = request.Name.Trim();
        rule.TriggerContext = request.TriggerContext.Trim();
        rule.AskName = request.AskName;
        rule.AskPhone = request.AskPhone;
        rule.AskEmail = request.AskEmail;
        rule.AskPreferredDate = request.AskPreferredDate;
        rule.AskPreferredTime = request.AskPreferredTime;
        rule.AskProcedureOfInterest = request.AskProcedureOfInterest;
        rule.IsActive = request.IsActive;

        await _leadCaptureRuleRepository.UpdateAsync(rule);

        return Ok(rule);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var rule = await _leadCaptureRuleRepository.GetByIdAsync(id);
        if (rule is null)
            return NotFound("Lead capture rule not found.");

        await _leadCaptureRuleRepository.DeactivateAsync(rule);

        return NoContent();
    }
}
