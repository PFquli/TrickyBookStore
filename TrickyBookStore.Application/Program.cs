using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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
                .BuildServiceProvider();

            //configure console logging
            var loggerFactoryService = serviceProvider.GetService<ILoggerFactory>();
            loggerFactoryService = LoggerFactory.Create(builder => builder.AddConsole());

            var logger = loggerFactoryService
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //Do thing here
        }
    }
}