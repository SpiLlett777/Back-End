using Laba12.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Laba12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Подключаемся через Dependency Injection к базе данных по строке подключения из appsettings.json
            builder.Services.AddDbContext<ProductDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("React-App", policy =>
                {
                    policy.WithOrigins("http://localhost:5173");    // Адрес, на котором локально запускается наше веб-приложение
                    policy.AllowAnyHeader();        // Разрешает использование любых заголовков в запросах.
                    policy.AllowAnyMethod();        // Разрешает использование любых HTTP-методов (GET, POST, PUT, DELETE и т.д.).
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("React-App");       // Применяем настройки CORS

            app.MapControllers();

            app.Run();
        }
    }
}
