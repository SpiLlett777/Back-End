namespace Laba1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // СОЗДАНИЕ ЭКЗЕМПЛЯРА WebApplication
            var builder = WebApplication.CreateBuilder(args);

            // РЕГИСТРАЦИЯ СЕРВИСОВ (осуществляется через Dependency Injection)
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // КОНФИГУРАЦИЯ middleware (например, маршрутизация, обработка статических файлов, обработка ошибок)
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            // ЗАПУСК ПРИЛОЖЕНИЯ
            app.Run();
        }
    }
}
