using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data_Driven_Final.Services;
using Data_Driven_Final.Models;
using System.Text.Json;

namespace Data_Driven_Final.Pages.Products
{
    public class AskModel : PageModel
    {
        private readonly GeminiService _gemini;
        private readonly ProductService _products;

        public AskModel(GeminiService gemini, ProductService products)
        {
            _gemini = gemini;
            _products = products;
        }

        [BindProperty]
        public string Question { get; set; } = string.Empty;

        public string? AiAnswer { get; set; }
        public List<Product> Results { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Question))
            {
                ModelState.AddModelError(string.Empty, "Please enter a question.");
                return Page();
            }

            // STEP 1 — Ask Gemini to interpret the question and return a JSON filter
            var filterJson = await _gemini.GetFilterJsonAsync(Question);

            // STEP 2 — Convert Gemini’s JSON into a filter object
            var filter = JsonSerializer.Deserialize<ProductQueryFilter>(
                filterJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            // STEP 3 — Query MongoDB using the interpreted filter
            Results = await _products.QueryAsync(
                filter?.Category,
                filter?.MaxPrice,
                filter?.SortBy,
                filter?.SortDirection
            );

            // STEP 4 — Ask Gemini to summarize the results in natural language
            AiAnswer = await _gemini.GetAnswerFromResultsAsync(Question, Results);

            return Page();
        }

        public class ProductQueryFilter
        {
            public string? Category { get; set; }
            public decimal? MaxPrice { get; set; }
            public string? SortBy { get; set; }
            public string? SortDirection { get; set; }
        }
    }
}
