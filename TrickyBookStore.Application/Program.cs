using Microsoft.Extensions.DependencyInjection;
using TrickyBookStore.Application.Controller;

namespace TrickyBookStore.Application
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Set up DI
            var serviceProvider = (ServiceProvider)new ServiceCollection()
                .AddTrickyBookStoreServices()
                .AddScoped<IPayment, Payment>()
                .BuildServiceProvider();

            var payment = serviceProvider.GetService<IPayment>();

            //Do thing here
            payment.StartProgram();
        }
    }
}