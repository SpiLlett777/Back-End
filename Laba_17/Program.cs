using Laba17.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

namespace Laba17
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

            builder.Services.AddMemoryCache();  // In-Memory ���
            builder.Services.AddDistributedMemoryCache();   // �������������� ���
            builder.Services.AddResponseCaching();  // ����������� HTTP-�������

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

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

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
