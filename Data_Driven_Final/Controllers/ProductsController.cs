using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Data_Driven_Final.Services;
using Data_Driven_Final.Models;

namespace Data_Driven_Final.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        // READ: List all products
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("🔥 ProductsController.Index HIT");

            var products = await _service.GetAllAsync();
            return View(products);
        }

        // CREATE: Show form
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: Submit form
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            // Parse Tags from comma-separated input
            var tagsInput = Request.Form["TagsInput"].ToString();
            product.Tags = tagsInput
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .ToList();

            // Ensure Specifications object exists
            if (product.Specifications == null)
                product.Specifications = new Specifications();

            await _service.CreateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // UPDATE: Show form
        public async Task<IActionResult> Edit(string id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // UPDATE: Submit form
        [HttpPost]
        public async Task<IActionResult> Edit(string id, Product updated)
        {
            if (!ModelState.IsValid)
                return View(updated);

            // Parse Tags from comma-separated input
            var tagsInput = Request.Form["TagsInput"].ToString();
            updated.Tags = tagsInput
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .ToList();

            // Ensure Specifications object exists
            if (updated.Specifications == null)
                updated.Specifications = new Specifications();

            await _service.UpdateAsync(id, updated);
            return RedirectToAction(nameof(Index));
        }

        // DELETE: Confirm page
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // DELETE: Submit
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
