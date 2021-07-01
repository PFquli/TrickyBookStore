using System;

namespace TrickyBookStore.Services.Shared
{
    public static class Utils
    {
        public static bool CheckIsFromDate(DateTimeOffset targetDate, DateTimeOffset fromDate)
        {
            return DateTimeOffset.Compare(targetDate, fromDate) > -1;
        }

        public static bool CheckIsToDate(DateTimeOffset targetDate, DateTimeOffset toDate)
        {
            return DateTimeOffset.Compare(targetDate, toDate) < 1;
        }
    }
}