using Loqim.Api.Data;
using Loqim.Api.Dtos;
using Loqim.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TenantsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TenantsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTenantRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Slug))
            return BadRequest("Slug is required.");

        var slugExists = await _context.Tenants.AnyAsync(x => x.Slug == request.Slug);
        if (slugExists)
            return Conflict("Slug already exists.");

        var tenant = new Tenant
        {
            Name = request.Name.Trim(),
            Slug = request.Slug.Trim().ToLower(),
            Status = "active",
            CreatedAt = DateTime.UtcNow
        };

        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = tenant.Id }, tenant);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == id);
        if (tenant is null)
            return NotFound();

        return Ok(tenant);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tenants = await _context.Tenants
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        return Ok(tenants);
    }
}