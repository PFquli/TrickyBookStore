using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;

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

        private bool CheckIsFromDate(DateTimeOffset targetDate, DateTimeOffset fromDate)
        {
            return DateTimeOffset.Compare(targetDate, fromDate) > -1;
        }

        private bool CheckIsToDate(DateTimeOffset targetDate, DateTimeOffset toDate)
        {
            return DateTimeOffset.Compare(targetDate, toDate) < 1;
        }

        private bool CheckIsRightCustomer(long targetId, long customerId)
        {
            return targetId == customerId;
        }

        private bool CheckIfTransactionIncluded(PurchaseTransaction pcTrans, long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            bool isRightCustomer = CheckIsRightCustomer(pcTrans.CustomerId, customerId);
            bool isRightDate = CheckIsFromDate(pcTrans.CreatedDate, fromDate) && CheckIsToDate(pcTrans.CreatedDate, toDate);
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