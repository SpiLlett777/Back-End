using Laba_6.Data;
using Laba_6.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace Laba_6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<UserDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SigningKey"]!)),
                        ValidateIssuerSigningKey = true
                    };
                });

            builder.Services.AddScoped<IAuthService, AuthService>();

            var signingKey = builder.Configuration.GetValue<string>("JwtSettings:SigningKey");
            var issuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer");
            var connection = builder.Configuration.GetValue<string>("ConnectionStrings:Default");

            Console.WriteLine($"Signing Key: {signingKey}");
            Console.WriteLine($"Issuer: {issuer}");
            Console.WriteLine($"Connection String: {connection}\n\n");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
