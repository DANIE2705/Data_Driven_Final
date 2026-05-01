using Data_Driven_Final.Models;
//using Data_Driven_Final.Services;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;


namespace Data_Driven_Final.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiOptions _options;

        public GeminiService(HttpClient httpClient, IOptions<GeminiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        // ⭐ THIS IS WHERE YOUR METHOD GOES
        public async Task<string> AskGeminiAsync(string prompt)
        {
            var url =
                $"https://generativelanguage.googleapis.com/v1/models/{_options.Model}:generateContent?key={_options.ApiKey}";

            var requestBody = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new { text = prompt }
                }
            }
        }
            };

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);
            var responseText = await response.Content.ReadAsStringAsync();

            // 🔥 Instead of EnsureSuccessStatusCode, throw the body so we SEE it
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseText);
            }

            return responseText;
        }


        public async Task<string> GetFilterJsonAsync(string userQuestion)
        {
                            string prompt =
                        $@"You are a MongoDB query builder.
                User question: {userQuestion}

                Return ONLY a JSON object with:
                - category (string or null)
                - maxPrice (number or null)
                - sortBy (string or null)
                - sortDirection (asc or desc)

                Example:
                {{ ""category"": ""Electronics"", ""maxPrice"": 500, ""sortBy"": ""Price"", ""sortDirection"": ""asc"" }}";

            return await AskGeminiAsync(prompt);
        }

        public async Task<string> GetAnswerFromResultsAsync(string question, IEnumerable<Product> products)
        {
            string productJson = JsonSerializer.Serialize(products);

            string prompt =
                    $@"User asked: {question}

            Here are the matching products:
            {productJson}

            Write a friendly natural-language explanation of the results.";

            return await AskGeminiAsync(prompt);
        }


    }
}
