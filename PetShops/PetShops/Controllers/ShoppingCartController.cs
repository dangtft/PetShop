using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShops.Data;
using PetShops.Interfaces;
using PetShops.Models;
using PetShops.Services;
using System.Security.Cryptography;

namespace PetShops.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly PetShopDbContext _context;
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository,IProductRepository productRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAllShoppingCartItems()
        {
            var items = _shoppingCartRepository.GetAllShoppingCartItems();
            return Ok(items);
        }

        [HttpGet]
        public IActionResult GetShoppingCartTotal()
        {
            var total = _shoppingCartRepository.GetShoppingCartTotal();
            return Ok(total);
        }

        [HttpPost("{pId}")]
        public async Task<IActionResult> AddToShoppingCart(int pId)
        {
            var product = await _productRepository.GetProductById(pId);
            if (product == null)
            {
                return NotFound($"Product with id {pId} not found.");
            }

            _shoppingCartRepository.AddToCart(product);
            return Ok("Product added to cart.");
        }

        [HttpDelete("{pId}")]
        public async Task<IActionResult> RemoveFromShoppingCart(int pId)
        {
            var product = await _productRepository.GetProductById(pId);
            if (product == null)
            {
                return NotFound($"Product with id {pId} not found.");
            }

            var quantity = _shoppingCartRepository.RemoveFromCart(product);
            return Ok(quantity);
        }

        [HttpDelete]
        public IActionResult ClearCart()
        {
            _shoppingCartRepository.ClearCart();
            return Ok("Shopping cart cleared.");
        }
    }
}
