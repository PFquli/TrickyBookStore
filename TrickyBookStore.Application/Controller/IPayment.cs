using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickyBookStore.Application.Controller
{
    internal interface IPayment
    {
        double GetCustomerBillForSpecificMonth(int year, int month, long customerId);

        void StartProgram();
    }
}