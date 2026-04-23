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
}
