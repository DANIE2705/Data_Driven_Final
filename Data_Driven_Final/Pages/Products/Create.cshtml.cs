using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data_Driven_Final.Models;
using Data_Driven_Final.Services;

namespace Data_Driven_Final.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ProductService _service;

        [BindProperty]
        public Product Product { get; set; } = new();

        [BindProperty]
        public string TagsInput { get; set; }

        [BindProperty]
        public string CategoriesInput { get; set; }

        public CreateModel(ProductService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            Console.WriteLine("ONGET FIRED");
            Product.Specifications = new Specifications();
            Product.Tags = new List<string>();
            Product.Categories = new List<string>();
            Product.Reviews = new List<Review>();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Product.Tags = TagsInput?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .ToList() ?? new List<string>();

            Product.Categories = CategoriesInput?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim())
                .ToList() ?? new List<string>();

            Product.Specifications ??= new Specifications();

            await _service.CreateAsync(Product);
            return RedirectToPage("Index");
        }
    }
}
