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

        public PurchaseTransactionService(IBookService bookService)
        {
            BookService = bookService;
        }

        private bool CheckIfTransactionIncluded(PurchaseTransaction pcTrans, long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            bool isRightCustomer = pcTrans.CustomerId == customerId;
            bool isRightDate = Utils.CheckIsFromDate(pcTrans.CreatedDate, fromDate) && Utils.CheckIsToDate(pcTrans.CreatedDate, toDate);
            return isRightCustomer && isRightDate;
        }

        public IList<PurchaseTransaction> GetPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            List<PurchaseTransaction> purchaseTransactionList = new List<PurchaseTransaction>();
            List<PurchaseTransaction> purchaseTransactionsData = Store.PurchaseTransactions.Data.ToList();
            foreach (PurchaseTransaction pcTrans in purchaseTransactionsData)
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