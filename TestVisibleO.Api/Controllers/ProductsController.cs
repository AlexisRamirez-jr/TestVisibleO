using Application.Application.DTOs;
using Application.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestVisibleO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllProductsAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (dto.Price <= 0) return BadRequest("El precio debe ser mayor que cero.");
            await _service.AddProductAsync(dto);
            return Ok();
        }

        [HttpPut("{id}/price")]
        public async Task<IActionResult> UpdatePrice(int id, [FromBody] UpdatePriceDto dto)
        {
            if (dto.Price <= 0) return BadRequest("El precio debe ser mayor que cero.");
            await _service.UpdatePriceAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProductAsync(id);
            return Ok();
        }
    }
}
