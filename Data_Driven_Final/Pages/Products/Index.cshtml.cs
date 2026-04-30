using Microsoft.AspNetCore.Mvc.RazorPages;
using Data_Driven_Final.Models;
using Data_Driven_Final.Services;

namespace Data_Driven_Final.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _service;

        public List<Product> Products { get; set; } = new();

        public IndexModel(ProductService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            Products = await _service.GetAllAsync();
        }
    }
}
