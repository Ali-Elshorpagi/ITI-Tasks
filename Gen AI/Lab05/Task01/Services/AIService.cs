using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Task01.Models;

namespace Task01.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly ILogger<AIService> _logger;

        public AIService(HttpClient httpClient, IConfiguration configuration, ILogger<AIService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            
            var n8nConfig = configuration.GetSection("N8nApi");
            var baseUrl = n8nConfig["BaseUrl"] ?? throw new ArgumentNullException("N8nApi:BaseUrl is missing in appsettings.json");
            _endpoint = n8nConfig["Endpoint"] ?? throw new ArgumentNullException("N8nApi:Endpoint is missing in appsettings.json");

            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<AIResponse> AskQuestionAsync(QuestionRequest request)
        {
            try
            {
                // Passing the question as "Greet" to match the webhook requirement
                var response = await _httpClient.PostAsJsonAsync(_endpoint, new { question = request.Question });

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var aiResponse = JsonConvert.DeserializeObject<AIResponse>(jsonString);

                        if (!string.IsNullOrWhiteSpace(aiResponse?.Answer))
                        {
                            return aiResponse;
                        }

                        return new AIResponse { Answer = "No response received" };
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Failed to parse API response. Response: {Response}", jsonString);
                        return new AIResponse { Answer = "Error contacting AI service" };
                    }
                }

                _logger.LogWarning("API returned status code: {StatusCode}", response.StatusCode);
                return new AIResponse { Answer = "Error contacting AI service" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while calling AI service");
                return new AIResponse { Answer = "Error contacting AI service" };
            }
        }
    }
}
