using Laba17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Laba17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ProductsController> _logger;
        private readonly IDistributedCache _distributedCache;
        private const string ProductsCacheKey = "ProductsList";

        public ProductsController(ProductDbContext context, IMemoryCache memoryCache, ILogger<ProductsController> logger, IDistributedCache distributedCache)
        {
            _context = context;
            _memoryCache = memoryCache;
            _logger = logger;
            _distributedCache = distributedCache;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            if (_memoryCache.TryGetValue(ProductsCacheKey, out List<Product> products))
            {
                _logger.LogWarning($"MemoryCache: найдено в кэше {products.Count} товаров");
            }
            else
            {
                _logger.LogWarning("MemoryCache: читаем из базы данных...");
                products = await _context.Products.ToListAsync();
                _memoryCache.Set(ProductsCacheKey, products, TimeSpan.FromSeconds(30));
                _logger.LogWarning($"MemoryCache: закэшировано {products.Count} товаров на 30 секунд");
            }

            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            string cacheKey = $"Product_{id}";
            Product product = null;
            var cached = await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cached))
            {
                _logger.LogWarning($"DistributedCache: найден в распределенном кэше товар с ключом {cacheKey}");
                product = JsonSerializer.Deserialize<Product>(cached)!;
            }
            else
            {
                _logger.LogWarning($"DistributedCache: в кэше не найден продукт по ключу {cacheKey}. Читаем из базы данных...");
                product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product is null)
                    return NotFound("Продукт не найден");

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
                };

                await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(product), options);
                _logger.LogWarning($"DistributedCache: продукт с ключом {cacheKey} закэширован на 60 секунд");
            }
            
            return Ok(product);
        }

        // GET: api/products/disk/{id}
        [HttpGet("disk/{id}")]
        public async Task<ActionResult<Product>> GetProductByIdDiskCache(int id)
        {
            var cacheFile = Path.Combine("CacheFiles", $"product_{id}.json");
            Product product = null;

            if (System.IO.File.Exists(cacheFile) && DateTime.UtcNow - System.IO.File.GetCreationTimeUtc(cacheFile) < TimeSpan.FromSeconds(45))
            {
                _logger.LogWarning($"DiskCache: найден файл {cacheFile}");
                var json = await System.IO.File.ReadAllTextAsync(cacheFile);
                product = JsonSerializer.Deserialize<Product>(json);
            }
            else
            {
                _logger.LogWarning($"DiskCache: в кэш диске не найден продукт (ID = {id}). Читаем из базы данных...");
                product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product is null)
                    return NotFound("Продукт не найден");

                Directory.CreateDirectory("CacheFiles");
                await System.IO.File.WriteAllTextAsync(cacheFile, JsonSerializer.Serialize(product));
                _logger.LogWarning($"DiskCache: записан файл {cacheFile}");
            }

            return Ok(product);
        }

        // GET: api/products/response
        [HttpGet("response")]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<List<Product>>> GetProductsResponseCache()
        {
            _logger.LogWarning($"ResponseCache: формируется свежий ответ в {DateTime.UtcNow}");
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // POST: /api/products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            _memoryCache.Remove(ProductsCacheKey);
            _distributedCache.Remove($"Product_{product.Id}");
            _logger.LogWarning($"Товар создан (Id = {product.Id}), кэши очищены");

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

            _memoryCache.Remove(ProductsCacheKey);
            _distributedCache.Remove($"Product_{id}");
            _logger.LogWarning($"Продукт (ID = {id}) обновлен, кэши очищены");

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

            _memoryCache.Remove(ProductsCacheKey);
            _distributedCache.Remove($"Product_{id}");
            _logger.LogWarning($"Продукт (ID = {id}) удален, кэши очищены");

            return NoContent();
        }
    }
}
