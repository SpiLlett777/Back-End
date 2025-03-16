using Laba12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Laba12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductsController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                return NotFound("Продукт с указанным ID не найден в базе данных");

            return Ok(product);
        }

        // POST: /api/products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // PUT: /api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                return NotFound("Продукт с указанным ID не найден в базе данных");

            if (updatedProduct.Name is not null && product.Name != updatedProduct.Name)
                product.Name = updatedProduct.Name;

            if (updatedProduct.Description is not null && product.Description != updatedProduct.Description)
                product.Description = updatedProduct.Description;

            if (updatedProduct.Price is not null && product.Price != updatedProduct.Price)
                product.Price = updatedProduct.Price;

            if (updatedProduct.Stock is not null && product.Stock != updatedProduct.Stock)
                product.Stock = updatedProduct.Stock;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                return NotFound("Продукт с указанным ID не найден в базе данных");

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
