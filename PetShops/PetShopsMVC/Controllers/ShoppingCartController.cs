﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShopsMVC.DTOs;
using PetShopsMVC.Models;
using PetShopsMVC.Models.Interfaces;
using System.Collections.Generic;

namespace PetShopsMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        
        private readonly HttpClient _httpClient;
        public ShoppingCartController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7182/api/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("ShoppingCart/GetAllShoppingCartItems");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error"); 
            }

            var content = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Carts>>(content);

            var totalItems = items.Sum(item => item.Qty);

            ViewBag.TotalCart = totalItems;
            return View(items);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int pId)
        {
            var response = await _httpClient.PostAsync($"ShoppingCart/AddToShoppingCart/{pId}", null);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            var cartResponse = await _httpClient.GetAsync("ShoppingCart/GetAllShoppingCartItems");
            if (cartResponse.IsSuccessStatusCode)
            {
                var content = await cartResponse.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<Carts>>(content);
                HttpContext.Session.SetInt32("CartCount", items.Count);
            }

            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveFromShoppingCart(int pId)
        {
            var response = await _httpClient.DeleteAsync($"ShoppingCart/RemoveFromShoppingCart/{pId}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            var cartResponse = await _httpClient.GetAsync("ShoppingCart/GetAllShoppingCartItems");
            if (cartResponse.IsSuccessStatusCode)
            {
                var content = await cartResponse.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<Carts>>(content);
                HttpContext.Session.SetInt32("CartCount", items.Count);
            }

            return RedirectToAction("Index");
        }
    }
}
