using Laba8.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

namespace Laba8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ProductDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("React-App", policy =>
                {
                    policy.WithOrigins("http://localhost:5173");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddMemoryCache();
            builder.Services.AddResponseCaching();

            builder.Logging.ClearProviders();

            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(), "Logs/structured-.json", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddOpenApi();

            builder.Services.AddDistributedMemoryCache();   // Распределенный кэш (требуется для хранения сессий)

            // Регистрация сессий с таймаутом 20 минут
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;     // защита от JS-доступа
                options.Cookie.IsEssential = true;  // обязательно для работы
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseSession();   // Подключение middleware сессий

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseCors("React-App");

            app.MapControllers();

            app.Run();
        }
    }
}
