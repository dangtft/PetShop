using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShopsMVC.Models;
using PetShopsMVC.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetShopsMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient _httpClient;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7182/api/");
        }

        public IActionResult Index()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(ContactDTO contactDto)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(contactDto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("Contact/AddContact", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index","Product");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while adding the contact.");
                }
            }
            return View(contactDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubscribers()
        {
            List<EmailSubscribe> subscribers = new List<EmailSubscribe>();
            HttpResponseMessage response = await _httpClient.GetAsync("Contact/GetAllSubscribers");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                subscribers = JsonConvert.DeserializeObject<List<EmailSubscribe>>(data);
            }

            return View(subscribers);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContact()
        {
            List<Contacts> contacts = new List<Contacts>();
            HttpResponseMessage response = await _httpClient.GetAsync("Contact/GetAllContacts");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                contacts = JsonConvert.DeserializeObject<List<Contacts>>(data);
            }

            return View(contacts);
        }
    }
}
