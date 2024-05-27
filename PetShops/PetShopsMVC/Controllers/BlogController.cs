using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShopsMVC.DTOs;
using static System.Reflection.Metadata.BlobBuilder;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using PetShopsMVC.Models.Interfaces;
using PetShopsMVC.Models;
using PetShopsMVC.ViewModel;

namespace PetShopsMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IApiService _apiService;
        public BlogController(IHttpClientFactory httpClientFactory, IApiService apiService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7182/api/");
            _apiService = apiService;
        }

        private void SetAuthorizationHeader()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Blogs> blogs = new List<Blogs>();
            HttpResponseMessage response = await _httpClient.GetAsync($"Blog/GetAllBlog");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                blogs = JsonConvert.DeserializeObject<List<Blogs>>(data);
            }
          
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            SetAuthorizationHeader();
            HttpResponseMessage response = await _httpClient.GetAsync($"Blog/GetBlog/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Blogs book = JsonConvert.DeserializeObject<Blogs>(data);
                return View(book);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BlogDTO blog)
        {
            SetAuthorizationHeader();

            var content = new StringContent(JsonConvert.SerializeObject(blog), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("Blog/AddBlog", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                return View(blog);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            SetAuthorizationHeader();

            HttpResponseMessage response = await _httpClient.GetAsync($"Blog/GetBlog/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                BlogDTO book = JsonConvert.DeserializeObject<BlogDTO>(data);
                return View(book);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BlogDTO book)
        {
            SetAuthorizationHeader();

            var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync($"Blog/UpdateBlog/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Không thể chỉnh sửa blog. Vui lòng thử lại sau.");
                return View(book);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            SetAuthorizationHeader();

            HttpResponseMessage response = await _httpClient.DeleteAsync($"Blog/DeleteBlog/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Book");
            }
            else
            {
                return NotFound();
            }
        }


    }
}
