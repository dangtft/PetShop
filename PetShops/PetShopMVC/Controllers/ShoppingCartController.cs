using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShopMVC.DTOs;

namespace PetShopMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7182/api/");
        }

        private void SetAuthorizationHeader()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
        public async Task<IActionResult> Index()
        {
            var items = await _httpClient.GetAsync("ShoppingCart/GetAllShoppingCartItems");
            ViewBag.TotalCart = await _httpClient.GetAsync("ShoppingCart/total");
            return View(items);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int pId)
        {
            var product = await _httpClient.GetAsync($"ShoppingCart/AddToShoppingCart/{pId}");
            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveFromShoppingCart(int pId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"ShoppingCart/RemoveFromCart/{pId}");
            return RedirectToAction("Index");
        }
    }
}
