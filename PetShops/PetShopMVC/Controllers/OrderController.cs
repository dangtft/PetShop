using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShopMVC.DTOs;

namespace PetShopMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7182/api/"); 
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderDTO order)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(order);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Order/PlaceOrder", data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home"); 
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error placing order: {ex.Message}";
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var response = await _httpClient.GetAsync("Order/GetAllOrders");
                response.EnsureSuccessStatusCode();
                var orders = await response.Content.ReadAsAsync<IEnumerable<OrderDTO>>();
                return View(orders);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error getting orders: {ex.Message}";
                return View("Error");
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Order/GetOrderById/{orderId}");
                response.EnsureSuccessStatusCode();
                var order = await response.Content.ReadAsAsync<OrderDTO>();
                return View(order);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error getting order: {ex.Message}";
                return View("Error");
            }
        }

        [HttpGet("{userId}/completed")]
        public async Task<IActionResult> GetCompletedOrders(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Order/GetCompletedOrders/{userId}");
                response.EnsureSuccessStatusCode();
                var completedOrders = await response.Content.ReadAsAsync<IEnumerable<OrderDTO>>();
                return View(completedOrders);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error getting completed orders: {ex.Message}";
                return View("Error");
            }
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Order/DeleteOrder/{orderId}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error deleting order: {ex.Message}";
                return View("Error");
            }
        }
    }
}
