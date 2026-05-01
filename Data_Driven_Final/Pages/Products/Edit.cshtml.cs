using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data_Driven_Final.Models;
using Data_Driven_Final.Services;

namespace Data_Driven_Final.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ProductService _service;

        [BindProperty]
        public Product Product { get; set; } = new();

        [BindProperty]
        public string TagsInput { get; set; }

        [BindProperty]
        public string CategoriesInput { get; set; }

        public EditModel(ProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var existing = await _service.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            Product = existing;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            Product.Specifications ??= new Specifications();
            Product.Specifications.Processor ??= new Processor();
            Product.Specifications.Graphics ??= new Graphics();
            Product.Specifications.Storage ??= new Storage();
            Product.Tags ??= new List<string>();
            Product.Categories ??= new List<string>();
            Product.Reviews ??= new List<Review>();

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

            await _service.UpdateAsync(id, Product);

            return RedirectToPage("Index");
        }
    }
}
