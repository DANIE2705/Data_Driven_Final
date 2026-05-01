using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data_Driven_Final.Models;
using Data_Driven_Final.Services;

namespace Data_Driven_Final.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ProductService _service;

        public Product? Product { get; set; }

        public DetailsModel(ProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Product = await _service.GetByIdAsync(id);

            if (Product == null)
                return NotFound();

            return Page();
        }
    }
}
