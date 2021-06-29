using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickyBookStore.Services.Shared
{
    public static class SharedService
    {
        public static bool CheckIsFromDate(DateTimeOffset targetDate, DateTimeOffset fromDate)
        {
            return DateTimeOffset.Compare(targetDate, fromDate) > -1;
        }

        public static bool CheckIsToDate(DateTimeOffset targetDate, DateTimeOffset toDate)
        {
            return DateTimeOffset.Compare(targetDate, toDate) < 1;
        }

        public static bool CheckIsRightCustomer(long targetId, long customerId)
        {
            return targetId == customerId;
        }
    }
}