using System;
using System.Linq;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.PricingPlans;
using TrickyBookStore.Models;
using System.Collections.Generic;

namespace TrickyBookStore.Services.Payment
{
    internal class PaymentService : IPaymentService
    {
        private ICustomerService CustomerService { get; }
        private IPurchaseTransactionService PurchaseTransactionService { get; }
        public IPricingPlanService PricingPlanService { get; }
        public IBookService BookService { get; }

        public PaymentService(ICustomerService customerService,
            IPurchaseTransactionService purchaseTransactionService, IPricingPlanService pricingPlanService, IBookService bookService)
        {
            CustomerService = customerService;
            PurchaseTransactionService = purchaseTransactionService;
            PricingPlanService = pricingPlanService;
            BookService = bookService;
        }

        private PricingPlan GetPricingPlanForCustomer(long customerId)
        {
            Customer customer = CustomerService.GetCustomerById(customerId);
            int[] subscriptionIds = customer.SubscriptionIds.ToArray();
            return PricingPlanService.GetPricingPlan(subscriptionIds);
        }

        private double GetPaymentForNewBookInSubscribedCategories(Book book, PricingPlan pricingPlan)
        {
            double paymentAmount = 0;
            int categoryId = book.CategoryId;
            int categoryIndex = pricingPlan.CategoryVouchers.IndexOf(categoryId);
            if (categoryIndex > -1)
            {
                paymentAmount += pricingPlan.CategoryDiscountRate * book.Price;
                pricingPlan.CategoryVouchers.RemoveAt(categoryIndex);
            }
            else
            {
                paymentAmount += GetPaymentForNewBookInOtherCategories(book, pricingPlan);
            }
            return paymentAmount;
        }

        private double GetPaymentForNewBookInOtherCategories(Book book, PricingPlan pricingPlan)
        {
            double paymentAmount = 0;
            if (pricingPlan.SortedGlobalVouchers.Count > 0)
            {
                double discountRate = pricingPlan.SortedGlobalVouchers[0];
                paymentAmount += discountRate * book.Price;
                pricingPlan.SortedGlobalVouchers.RemoveAt(0);
            }
            else
            {
                paymentAmount += pricingPlan.FullPriceRate * book.Price;
            }
            return paymentAmount;
        }

        /// <summary>
        /// Check if book's category id is inside subscribed category ids
        /// Apply Category read rate if so, Global read rate otherwise.
        /// </summary>
        /// <param name="book">
        /// Current old book in the transaction list
        /// </param>
        /// <param name="pricingPlan">
        /// Pricing plan for current customer
        /// </param>
        /// <returns>
        /// Payment amount for this old book
        /// </returns>
        private double GetPaymentForNewBook(Book book, PricingPlan pricingPlan)
        {
            double paymentAmount = 0;
            List<int> subscribedCategoryIds = pricingPlan.UniqueCategoryIds;
            int categoryId = book.CategoryId;
            if (subscribedCategoryIds.IndexOf(categoryId) > -1)
            {
                paymentAmount += GetPaymentForNewBookInSubscribedCategories(book, pricingPlan);
            }
            else
            {
                paymentAmount += GetPaymentForNewBookInOtherCategories(book, pricingPlan);
            }
            return paymentAmount;
        }

        /// <summary>
        /// Check if book's category id is inside subscribed category ids
        /// Apply Category read rate if so, Global read rate otherwise.
        /// </summary>
        /// <param name="book">
        /// Current old book in the transaction list
        /// </param>
        /// <param name="pricingPlan">
        /// Pricing plan for current customer
        /// </param>
        /// <returns>
        /// Payment amount for this old book
        /// </returns>
        private double GetPaymentForOldBook(Book book, PricingPlan pricingPlan)
        {
            double paymentAmount = 0;
            List<int> subscribedCategoryIds = pricingPlan.UniqueCategoryIds;
            int categoryId = book.CategoryId;
            if (subscribedCategoryIds.IndexOf(categoryId) > -1)
            {
                paymentAmount += pricingPlan.CategoryReadRate * book.Price;
            }
            else
            {
                paymentAmount += pricingPlan.GlobalReadRate * book.Price;
            }
            return paymentAmount;
        }

        public double GetPaymentAmount(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            double paymentAmount = 0;
            List<PurchaseTransaction> purchaseTransactions = PurchaseTransactionService.GetPurchaseTransactions(customerId, fromDate, toDate).ToList();
            long[] bookIds = (from transaction in purchaseTransactions
                              select transaction.BookId
                              ).ToArray();
            List<Book> books = BookService.GetBooks(bookIds).ToList();
            PricingPlan pricingPlan = GetPricingPlanForCustomer(customerId);
            foreach (Book book in books)
            {
                if (book.IsOld)
                {
                    paymentAmount += GetPaymentForOldBook(book, pricingPlan);
                }
                else
                {
                    paymentAmount += GetPaymentForNewBook(book, pricingPlan);
                }
            }
            return paymentAmount;
        }
    }
}