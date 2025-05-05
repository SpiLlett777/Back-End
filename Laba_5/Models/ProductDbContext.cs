using Microsoft.EntityFrameworkCore;

namespace Laba5.Models
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Ноутбук",
                    Description = "Высокопроизводительный ноутбук с 16 ГБ оперативной памяти и SSD на 512 ГБ",
                    Price = 999.99m,
                    Stock = 10
                },
                new Product
                {
                    Id = 2,
                    Name = "Смартфон",
                    Description = "Последняя модель смартфона с 128 ГБ встроенной памяти",
                    Price = 499.99m,
                    Stock = 20
                },
                new Product
                {
                    Id = 3,
                    Name = "Планшет",
                    Description = "10-дюймовый планшет с 64 ГБ встроенной памяти",
                    Price = 299.99m,
                    Stock = 15
                },
                new Product
                {
                    Id = 4,
                    Name = "Умные часы",
                    Description = "Умные часы с монитором сердечного ритма и GPS",
                    Price = 199.99m,
                    Stock = 30
                }
            );
        }
    }
}
