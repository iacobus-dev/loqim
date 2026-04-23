using Loqim.Api.Dtos;
using Loqim.Domain.Entities;
using Loqim.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Loqim.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogProductController : ControllerBase
{
    private readonly ICatalogProductRepository _catalogProductRepository;
    private readonly ITenantRepository _tenantRepository;

    public CatalogProductController(
        ICatalogProductRepository catalogProductRepository,
        ITenantRepository tenantRepository)
    {
        _catalogProductRepository = catalogProductRepository;
        _tenantRepository = tenantRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCatalogProductRequest request)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
        if (tenant is null)
            return NotFound("Tenant not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Description))
            return BadRequest("Description is required.");

        var product = new CatalogProduct
        {
            TenantId = request.TenantId,
            Name = request.Name.Trim(),
            Description = request.Description.Trim(),
            PurchaseLink = request.PurchaseLink.Trim(),
            IsActive = true
        };

        await _catalogProductRepository.AddAsync(product);

        return Ok(product);
    }

    [HttpGet("tenant/{tenantId:guid}")]
    public async Task<IActionResult> GetByTenantId(Guid tenantId)
    {
        var products = await _catalogProductRepository.GetActiveByTenantIdAsync(tenantId);

        return Ok(products);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCatalogProductRequest request)
    {
        var product = await _catalogProductRepository.GetByIdAsync(id);
        if (product is null)
            return NotFound("Catalog product not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Description))
            return BadRequest("Description is required.");

        product.Name = request.Name.Trim();
        product.Description = request.Description.Trim();
        product.PurchaseLink = request.PurchaseLink.Trim();
        product.IsActive = request.IsActive;

        await _catalogProductRepository.UpdateAsync(product);

        return Ok(product);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _catalogProductRepository.GetByIdAsync(id);
        if (product is null)
            return NotFound("Catalog product not found.");

        await _catalogProductRepository.DeleteAsync(product);

        return NoContent();
    }
}
