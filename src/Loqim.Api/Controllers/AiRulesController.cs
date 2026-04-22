using Loqim.Api.Data;
using Loqim.Api.Dtos;
using Loqim.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiRulesController : ControllerBase
{
    private readonly AppDbContext _context;

    public AiRulesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAiRuleRequest request)
    {
        var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == request.TenantId);
        if (tenant is null)
            return NotFound("Tenant not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Content))
            return BadRequest("Content is required.");

        var rule = new AiRule
        {
            TenantId = request.TenantId,
            Name = request.Name.Trim(),
            Content = request.Content.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.AiRules.Add(rule);
        await _context.SaveChangesAsync();

        return Ok(rule);
    }

    [HttpGet("tenant/{tenantId:guid}")]
    public async Task<IActionResult> GetByTenant(Guid tenantId)
    {
        var rules = await _context.AiRules
            .Where(x => x.TenantId == tenantId && x.IsActive)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        return Ok(rules);
    }
}