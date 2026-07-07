using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Chat.Desktop.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        public string JwtToken { get; set; } = string.Empty;

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };
            _http = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7000/")
            };
        }

        private void AttachToken()
        {
            if (!string.IsNullOrEmpty(JwtToken))
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", JwtToken);
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            AttachToken();
            var response = await _http.GetAsync(url);
            if (!response.IsSuccessStatusCode) return default;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<(bool Success, string Content)> PostAsync(string url, object body)
        {
            AttachToken();
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            return (response.IsSuccessStatusCode, responseContent);
        }

        public async Task<bool> DeleteAsync(string url)
        {
            AttachToken();
            var response = await _http.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
