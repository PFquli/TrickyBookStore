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

            var logger = loggerFactoryService
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            var payment = serviceProvider.GetService<IPayment>();

            //Do thing here
            payment.StartProgram();
        }
    }
}