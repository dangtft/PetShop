using PetShopsMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShopsMVC.ViewModel;
using System.Text;

namespace PetShopsMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SetAuthorizationHeader();
            List<Products> products = new List<Products>();
            HttpResponseMessage response = await _httpClient.GetAsync($"Product/GetAllProduct");
            List<Blogs> blogs = new List<Blogs>();
            HttpResponseMessage response2 = await _httpClient.GetAsync($"Blog/GetAllBlog");

            if (response.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                string data = await response2.Content.ReadAsStringAsync();
                blogs = JsonConvert.DeserializeObject<List<Blogs>>(data);
                string data2 = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Products>>(data2);
            }

          
            var viewModel = new HomeVM
            {
               Products = products ?? new List<Products>(),
               Blogs = blogs ?? new List<Blogs>()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Shop(string searchTerm)
        {
            List<Products> products = new List<Products>();
            HttpResponseMessage response;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                response = await _httpClient.GetAsync($"Product/GetAllProduct");
            }
            else
            {
                response = await _httpClient.GetAsync($"Product/SearchProductsByName?productName={searchTerm}");
            }

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(data))
                {
                    products = JsonConvert.DeserializeObject<List<Products>>(data);
                }
            }

            ViewBag.SearchTerm = searchTerm;
            if (products == null || !products.Any())
            {
                return RedirectToAction("Shop"); 
            }
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Product/GetProduct/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Products product = JsonConvert.DeserializeObject<Products>(data);
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Product/DeleteProduct/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EmailSubscribe(string email)
        {
            try
            {
                var emailSubscription = new { Email = email };
                var content = new StringContent(JsonConvert.SerializeObject(emailSubscription), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Product/EmailSubscribe", content);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.SuccessMessage = "Cảm ơn bạn đã đăng ký!";
                    return RedirectToAction(nameof(Index)); 
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Không thể thêm đăng ký email.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý yêu cầu của bạn.");
            }
        }



    }
}
