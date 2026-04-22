using Loqim.Api.Data;
using Loqim.Api.Dtos;
using Loqim.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessProfilesController : ControllerBase
{
    private readonly AppDbContext _context;

    public BusinessProfilesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBusinessProfileRequest request)
    {
        var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == request.TenantId);
        if (tenant is null)
            return NotFound("Tenant not found.");

        var existingProfile = await _context.BusinessProfiles
            .FirstOrDefaultAsync(x => x.TenantId == request.TenantId);

        if (existingProfile is not null)
            return Conflict("Business profile already exists for this tenant.");

        var profile = new BusinessProfile
        {
            TenantId = request.TenantId,
            Segment = request.Segment.Trim(),
            Description = request.Description.Trim(),
            ToneOfVoice = request.ToneOfVoice.Trim(),
            MainGoal = request.MainGoal.Trim(),
            BusinessHours = request.BusinessHours.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _context.BusinessProfiles.Add(profile);
        await _context.SaveChangesAsync();

        return Ok(profile);
    }

    [HttpGet("{tenantId:guid}")]
    public async Task<IActionResult> GetByTenant(Guid tenantId)
    {
        var profile = await _context.BusinessProfiles
            .FirstOrDefaultAsync(x => x.TenantId == tenantId);

        if (profile is null)
            return NotFound();

        return Ok(profile);
    }
}