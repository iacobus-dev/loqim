using Loqim.Api.Dtos;
using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogServiceController : ControllerBase
{
    private readonly ICatalogServiceItemRepository _catalogServiceItemRepository;
    private readonly ITenantRepository _tenantRepository;

    public CatalogServiceController(
        ICatalogServiceItemRepository catalogServiceItemRepository,
        ITenantRepository tenantRepository)
    {
        _catalogServiceItemRepository = catalogServiceItemRepository;
        _tenantRepository = tenantRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCatalogServiceItemRequest request)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
        if (tenant is null)
            return NotFound("Tenant not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Description))
            return BadRequest("Description is required.");

        var serviceItem = new CatalogServiceItem
        {
            TenantId = request.TenantId,
            Name = request.Name.Trim(),
            Description = request.Description.Trim(),
            RequiresScheduling = request.RequiresScheduling,
            BookingLink = request.BookingLink.Trim(),
            IsActive = true
        };

        await _catalogServiceItemRepository.AddAsync(serviceItem);

        return Ok(serviceItem);
    }

    [HttpGet("tenant/{tenantId:guid}")]
    public async Task<IActionResult> GetByTenantId(Guid tenantId)
    {
        var serviceItems = await _catalogServiceItemRepository.GetActiveByTenantIdAsync(tenantId);

        return Ok(serviceItems);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCatalogServiceItemRequest request)
    {
        var serviceItem = await _catalogServiceItemRepository.GetByIdAsync(id);
        if (serviceItem is null)
            return NotFound("Catalog service item not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Description))
            return BadRequest("Description is required.");

        serviceItem.Name = request.Name.Trim();
        serviceItem.Description = request.Description.Trim();
        serviceItem.RequiresScheduling = request.RequiresScheduling;
        serviceItem.BookingLink = request.BookingLink.Trim();
        serviceItem.IsActive = request.IsActive;

        await _catalogServiceItemRepository.UpdateAsync(serviceItem);

        return Ok(serviceItem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var serviceItem = await _catalogServiceItemRepository.GetByIdAsync(id);
        if (serviceItem is null)
            return NotFound("Catalog service item not found.");

        await _catalogServiceItemRepository.DeleteAsync(serviceItem);

        return NoContent();
    }
}
