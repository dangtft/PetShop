using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using PetShopsMVC.Models;
using PetShopsMVC.DTOs;
using PetShopsMVC.ViewModel;
using PetShopsMVC.Models.Interfaces;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

namespace PetShopsMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        public AdminController(IHttpClientFactory httpClientFactory)
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
            List<Contacts> contacts = new List<Contacts>();
            List<EmailSubscribe> subscribers = new List<EmailSubscribe>();
            HttpResponseMessage response = await _httpClient.GetAsync("Contact/GetAllSubscribers");
            HttpResponseMessage response2 = await _httpClient.GetAsync("Contact/GetAllContacts");

            if (response.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                subscribers = JsonConvert.DeserializeObject<List<EmailSubscribe>>(data);

                string data2 = await response2.Content.ReadAsStringAsync();
                contacts = JsonConvert.DeserializeObject<List<Contacts>>(data2);
            }

            var viewModel = new ContactSubscriberViewModel
            {
                Contacts = contacts,
                Subscribers = subscribers
            };

            return View(viewModel);
        }


        #region Blog

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            List<Blogs> blogs = new List<Blogs>();

            HttpResponseMessage response = await _httpClient.GetAsync("Blog/GetAllBlog");
            if (response.IsSuccessStatusCode)
            {
                blogs = await response.Content.ReadFromJsonAsync<List<Blogs>>();
            }

            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> GetBlog(int id)
        {
            Blogs blog = null;

            HttpResponseMessage response = await _httpClient.GetAsync($"Blog/GetBlog/{id}");
            if (response.IsSuccessStatusCode)
            {
                blog = await response.Content.ReadFromJsonAsync<Blogs>();
            }

            return View(blog);
        }

        public IActionResult AddBlog()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBlog(Blogs blogDTO)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Blog/AddBlog", blogDTO);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetAllBlogs");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBlog(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Blog/GetBlog/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var blog = JsonConvert.DeserializeObject<Blogs>(data);
                return View(blog);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBlog(int id, Blogs blogDTO)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Blog/UpdateBlog/{id}", blogDTO);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetAllBlogs");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Blog/DeleteBlog/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetAllBlogs");
        }
        #endregion Blog

        #region Order

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            List<Orders> orders = new List<Orders>();

            HttpResponseMessage response = await _httpClient.GetAsync("Order/GetAllOrders");
            if (response.IsSuccessStatusCode)
            {
                orders = await response.Content.ReadFromJsonAsync<List<Orders>>();
                return View(orders);
            }
            else
            {
                return StatusCode((int)response.StatusCode, $"Error fetching orders: {response.ReasonPhrase}");
            }
        }

       

       

        [HttpGet]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Order/DeleteOrder/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllOrders));
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                return StatusCode((int)response.StatusCode, $"Error deleting order: {response.ReasonPhrase}");
            }
        }

        #endregion Order

        #region Product

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            List<Products> products = new List<Products>();
            HttpResponseMessage response = await _httpClient.GetAsync("Product/GetAllProduct");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Products>>(data);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving products");
            }

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Product/GetProduct/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Products>(data);
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Products productDTO)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Product/AddProduct", productDTO);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Products));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error adding product");
                return View(productDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Product/GetProduct/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Products>(data);
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, Products productDTO)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Product/UpdateProduct/{id}", productDTO);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Products));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error updating product");
                return View(productDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Product/DeleteProduct/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Products));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error deleting product");
                return RedirectToAction(nameof(Products));
            }
        }

        #endregion

        #region Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Admin/AllUsersAndRoles"); 
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserWithRolesVM>>(data);
                return View(users);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving users");
                return View(new List<UserWithRolesVM>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(string userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Admin/UserDetails/{userId}"); 
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserWithRolesVM>(data);
                return View(user);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving user");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRoles(string userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Admin/EditUserRoles/{userId}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<UserWithRolesVM>(data);
                return View(model);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving user roles");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRoles(UserWithRolesVM model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("Admin/EditUserRoles", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllUsers)); 
            }
            else
            {
                string errorData = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error updating user roles: {errorData}");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Admin/DeleteUser/{userId}"); 
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllUsers));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error deleting user");
                return RedirectToAction(nameof(GetAllUsers));
            }
        }


        #endregion Users
    }
}
