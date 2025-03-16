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

            // ������������ ����� Dependency Injection � ���� ������ �� ������ ����������� �� appsettings.json
            builder.Services.AddDbContext<ProductDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("React-App", policy =>
                {
                    policy.WithOrigins("http://localhost:5173");    // �����, �� ������� �������� ����������� ���� ���-����������
                    policy.AllowAnyHeader();        // ��������� ������������� ����� ���������� � ��������.
                    policy.AllowAnyMethod();        // ��������� ������������� ����� HTTP-������� (GET, POST, PUT, DELETE � �.�.).
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

            app.UseCors("React-App");       // ��������� ��������� CORS

            app.MapControllers();

            app.Run();
        }
    }
}
