using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickyBookStore.Application.Controller
{
    internal interface IPayment
    {
        long GetPaymentOnAMonthForUser(int year, int month, int customerId);
    }
}