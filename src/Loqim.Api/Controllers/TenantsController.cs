using Loqim.Api.Dtos;
using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TenantsController : ControllerBase
{
    private readonly ITenantRepository _tenantRepository;

    public TenantsController(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTenantRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Slug))
            return BadRequest("Slug is required.");

        var normalizedSlug = request.Slug.Trim().ToLower();

        var slugExists = await _tenantRepository.SlugExistsAsync(normalizedSlug);
        if (slugExists)
            return Conflict("Slug already exists.");

        var tenant = new Tenant
        {
            Name = request.Name.Trim(),
            Slug = normalizedSlug,
            Status = "active",
            CreatedAt = DateTime.UtcNow
        };

        await _tenantRepository.AddAsync(tenant);

        return CreatedAtAction(nameof(GetById), new { id = tenant.Id }, tenant);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        if (tenant is null)
            return NotFound();

        return Ok(tenant);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tenants = await _tenantRepository.GetAllAsync();

        return Ok(tenants);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTenantRequest request)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        if (tenant is null)
            return NotFound();

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Slug))
            return BadRequest("Slug is required.");

        if (string.IsNullOrWhiteSpace(request.Status))
            return BadRequest("Status is required.");

        var normalizedSlug = request.Slug.Trim().ToLower();
        var slugExists = await _tenantRepository.SlugExistsAsync(normalizedSlug, tenant.Id);
        if (slugExists)
            return Conflict("Slug already exists.");

        tenant.Name = request.Name.Trim();
        tenant.Slug = normalizedSlug;
        tenant.Status = request.Status.Trim().ToLower();

        await _tenantRepository.UpdateAsync(tenant);

        return Ok(tenant);
    }
}
