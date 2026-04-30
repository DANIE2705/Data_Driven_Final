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
        public Product Product { get; set; }

        [BindProperty]
        public string TagsInput { get; set; }

        public EditModel(ProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Product = await _service.GetByIdAsync(id);

            if (Product == null)
                return NotFound();

            TagsInput = Product.Tags != null ? string.Join(", ", Product.Tags) : "";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
                return Page();

            Product.Tags = TagsInput?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .ToList() ?? new List<string>();

            Product.Specifications ??= new Specifications();

            await _service.UpdateAsync(id, Product);
            return RedirectToPage("Index");
        }
    }
}
