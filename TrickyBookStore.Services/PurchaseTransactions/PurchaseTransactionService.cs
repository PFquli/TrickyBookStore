using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Shared;

namespace TrickyBookStore.Services.PurchaseTransactions
{
    internal class PurchaseTransactionService : IPurchaseTransactionService
    {
        private IBookService BookService { get; }

        private readonly List<PurchaseTransaction> _purchaseTransactions;

        public PurchaseTransactionService(IBookService bookService)
        {
            BookService = bookService;
            _purchaseTransactions = Store.PurchaseTransactions.Data.ToList();
        }

        private bool CheckIfTransactionIncluded(PurchaseTransaction pcTrans, long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            bool isRightCustomer = SharedService.CheckIsRightCustomer(pcTrans.CustomerId, customerId);
            bool isRightDate = SharedService.CheckIsFromDate(pcTrans.CreatedDate, fromDate) && SharedService.CheckIsToDate(pcTrans.CreatedDate, toDate);
            return isRightCustomer && isRightDate;
        }

        public IList<PurchaseTransaction> GetPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            List<PurchaseTransaction> purchaseTransactionList = new List<PurchaseTransaction>();
            foreach (PurchaseTransaction pcTrans in _purchaseTransactions)
            {
                if (CheckIfTransactionIncluded(pcTrans, customerId, fromDate, toDate))
                {
                    purchaseTransactionList.Add(pcTrans);
                }
            }
            return purchaseTransactionList;
        }
    }
}