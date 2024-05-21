using Microsoft.AspNetCore.Mvc;
using PetShops.Data;
using PetShops.DTOs;
using PetShops.Interfaces;
using PetShops.Models;

namespace PetShops.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly PetShopDbContext _context;
        private readonly IProductRepository _productRepository;

        public ProductController(PetShopDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productRepository.GetProductsAsync();
            if (products == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No product found matching the criteria.");
            }
            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No product found for id: {id}");
            }
            return StatusCode(StatusCodes.Status200OK, product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(ProductDTO productDTO)
        {
            var result = await _productRepository.AddProduct(productDTO);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add product.");
            }

            return StatusCode(StatusCodes.Status200OK, "Product added successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDTO)
        {
            var result = await _productRepository.UpdateProduct(id, productDTO);

            if (!result)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProduct(id);

            if (!result)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, "Product deleted successfully");
        }
    }
}
