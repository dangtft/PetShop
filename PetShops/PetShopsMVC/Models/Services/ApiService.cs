using Newtonsoft.Json;
using System.Text;
using PetShopsMVC.Models.Interfaces;

namespace PetShopsMVC.Models.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;

        public ApiService(IHttpClientFactory httpClientFactory, ILogger<ApiService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");
            _logger.LogInformation("Gửi yêu cầu lấy token đến {url}", "https://localhost:7182/api/auth/login");
            var response = await _httpClient.PostAsync("https://localhost:7182/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseData);
                return tokenResponse.Token;
            }
            _logger.LogError("Yêu cầu lấy token thất bại với mã trạng thái {statusCode}", response.StatusCode);
            return null;
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }

}
