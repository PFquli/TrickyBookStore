using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;
using TrickyBookStore.Services.PricingPlans;
using TrickyBookStore.Services.Payment;

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