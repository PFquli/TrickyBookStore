using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PricingPlans;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TrickyBookStoreServiceCollection
    {
        public static IServiceCollection AddTrickyBookStoreServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPurchaseTransactionService, PurchaseTransactionService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddTransient<IPricingPlanService, PricingPlanService>();
            services.AddTransient<IPaymentService, PaymentService>();
            return services;
        }
    }
}