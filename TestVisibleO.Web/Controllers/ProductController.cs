using Microsoft.AspNetCore.Mvc;
using TestVisibleO.Application.DTOs;
using TestVisibleO.Application.Interfaces;
using TestVisibleO.Domain.Interfaces;

namespace TestVisibleO.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllProductsAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.AddProductAsync(dto);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var dto = new UpdateProductDto
            {
                Id = product.Id,
                Name = product.NameProduct,
                Description = product.DescriptionProduct,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.UpdateProductAsync(dto);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
