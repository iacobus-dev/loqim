using Loqim.Api.Dtos;
using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessProfilesController : ControllerBase
{
    private readonly IBusinessProfileRepository _businessProfileRepository;
    private readonly ITenantRepository _tenantRepository;

    public BusinessProfilesController(
        IBusinessProfileRepository businessProfileRepository,
        ITenantRepository tenantRepository)
    {
        _businessProfileRepository = businessProfileRepository;
        _tenantRepository = tenantRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBusinessProfileRequest request)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
        if (tenant is null)
            return NotFound("Tenant not found.");

        var existingProfile = await _businessProfileRepository.GetByTenantIdAsync(request.TenantId);

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

        await _businessProfileRepository.AddAsync(profile);

        return Ok(profile);
    }

    [HttpGet("{tenantId:guid}")]
    public async Task<IActionResult> GetByTenant(Guid tenantId)
    {
        var profile = await _businessProfileRepository.GetByTenantIdAsync(tenantId);

        if (profile is null)
            return NotFound();

        return Ok(profile);
    }

    [HttpPut("{tenantId:guid}")]
    public async Task<IActionResult> Update(Guid tenantId, [FromBody] UpdateBusinessProfileRequest request)
    {
        var profile = await _businessProfileRepository.GetByTenantIdAsync(tenantId);
        if (profile is null)
            return NotFound("Business profile not found.");

        if (string.IsNullOrWhiteSpace(request.Segment))
            return BadRequest("Segment is required.");

        if (string.IsNullOrWhiteSpace(request.Description))
            return BadRequest("Description is required.");

        if (string.IsNullOrWhiteSpace(request.ToneOfVoice))
            return BadRequest("ToneOfVoice is required.");

        if (string.IsNullOrWhiteSpace(request.MainGoal))
            return BadRequest("MainGoal is required.");

        if (string.IsNullOrWhiteSpace(request.BusinessHours))
            return BadRequest("BusinessHours is required.");

        profile.Segment = request.Segment.Trim();
        profile.Description = request.Description.Trim();
        profile.ToneOfVoice = request.ToneOfVoice.Trim();
        profile.MainGoal = request.MainGoal.Trim();
        profile.BusinessHours = request.BusinessHours.Trim();

        await _businessProfileRepository.UpdateAsync(profile);

        return Ok(profile);
    }
}
