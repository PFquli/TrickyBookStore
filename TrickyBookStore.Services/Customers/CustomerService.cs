using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.Services.Customers
{
    internal class CustomerService : ICustomerService
    {
        private ISubscriptionService SubscriptionService { get; }

        private readonly List<Customer> _customers;

        public CustomerService(ISubscriptionService subscriptionService)
        {
            SubscriptionService = subscriptionService;
            _customers = Store.Customers.Data.ToList();
        }

        public Customer GetCustomerById(long id)
        {
            Customer customer = _customers.FirstOrDefault(ctm => ctm.Id == id);
            return customer;
        }
    }
}