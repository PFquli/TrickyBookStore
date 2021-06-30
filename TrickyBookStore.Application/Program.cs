using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TrickyBookStore.Application.Controller;
using TrickyBookStore.Services.Books;

namespace TrickyBookStore.Application
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Set up DI
            var serviceProvider = (ServiceProvider)new ServiceCollection()
                .AddLogging()
                //Add services in between
                .AddTrickyBookStoreServices()
                .AddScoped<IPayment, Payment>()
                .BuildServiceProvider();

            //configure console logging
            var loggerFactoryService = serviceProvider.GetService<ILoggerFactory>();
            loggerFactoryService = LoggerFactory.Create(builder => builder.AddConsole());

            // Writeline debug, move to Payment.StartProgram later
            var books = serviceProvider.GetService<IBookService>();
            var listOfBooks = books.GetBooks();
            Console.WriteLine("Bellow is a list of books");
            Console.WriteLine(listOfBooks[0].Price);

            var logger = loggerFactoryService
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //Do thing here
        }
    }
}