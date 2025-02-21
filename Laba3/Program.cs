using System.Text;

// Без инверсии управления через Dependency Injection
//using Without_IoC;

// Dependency Injection через конструкторы
//using DI_Constructor;

// Dependency Injection через сеттеры
//using DI_Setter;

// Dependency Injection через интерфейс
//using DI_Interface;

// Dependency Injection через библиотеку 
using DependencyInjection_Framework;
using Microsoft.Extensions.DependencyInjection;

namespace Laba3
{
    internal class Program
    {
        static void Main(string[] args)
        {
     // Создаем коллекцию сервисов
            var serviceProvider = new ServiceCollection();

     // Регистрируем каждый класс инструментов
            serviceProvider.AddTransient<Hammer>();
            serviceProvider.AddTransient<Wrench>();
            serviceProvider.AddTransient<Saw>();
            
     // Регистрируем Builder, явно указывая, какие реализации ITool использовать
            serviceProvider.AddTransient<Builder>(sp => new Builder(
                sp.GetRequiredService<Hammer>(),
                sp.GetRequiredService<Wrench>(),
                sp.GetRequiredService<Saw>()
            ));

     // Строим провайдер сервисов
            var provider = serviceProvider.BuildServiceProvider();

    // Получаем экземпляр Builder и вызываем его метод Build
            Builder builder = provider.GetService<Builder>()!;

            builder.Build();
        }
    }
}