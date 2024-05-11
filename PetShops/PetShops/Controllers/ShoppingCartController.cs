using Microsoft.AspNetCore.Mvc;
using PetShops.Interfaces;
using PetShops.Models;

namespace PetShops.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpPost]
        public IActionResult AddToCart(Product product)
        {
            try
            {
                _shoppingCartRepository.AddToCart(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveFromCart(int id)
        {
            try
            {
                var product = new Product { ProductId = id }; 
                var removedQty = _shoppingCartRepository.RemoveFromCart(product);
                return Ok(removedQty);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllShoppingCartItems()
        {
            try
            {
                var shoppingCartItems = _shoppingCartRepository.GetAllShoppingCartItems();
                return Ok(shoppingCartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("total")]
        public IActionResult GetShoppingCartTotal()
        {
            try
            {
                var total = _shoppingCartRepository.GetShoppingCartTotal();
                return Ok(total);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("clear")]
        public IActionResult ClearCart()
        {
            try
            {
                _shoppingCartRepository.ClearCart();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
