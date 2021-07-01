using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyBookStore.Services.Payment;

namespace TrickyBookStore.Application.Controller
{
    internal class Payment : IPayment
    {
        private readonly IPaymentService paymentService;

        public Payment(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public DateTimeOffset CreateStartTime(int year, int month)
        {
            DateTime startOfMonth = new DateTime(year, month, 1);
            return new DateTimeOffset(startOfMonth);
        }

        public DateTimeOffset CreateEndTime(int year, int month)
        {
            int monthEnd = GetMonthEnd(year, month);
            DateTime endOfMonth = new DateTime(year, month, monthEnd);
            return new DateTimeOffset(endOfMonth);
        }

        public int GetMonthEnd(int year, int month)
        {
            int monthEnd = 31;
            switch (month)
            {
                case 2:
                    if (CheckIsLeapYear(year))
                        monthEnd = 29;
                    else
                        monthEnd = 28;
                    break;

                case 4:
                case 6:
                case 9:
                case 11:
                    monthEnd = 30;
                    break;
            }
            return monthEnd;
        }

        public bool CheckIsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }

        public double GetCustomerBillForSpecificMonth(int year, int month, long customerId)
        {
            DateTimeOffset fromDate = CreateStartTime(year, month);
            DateTimeOffset toDate = CreateEndTime(year, month);
            double paymentAmount = paymentService.GetPaymentAmount(customerId, fromDate, toDate);
            return paymentAmount;
        }

        public void StartProgram()
        {
            // Todo: setup UI here
            // Writeline, Readline, call GetCustomerBillForSpecificMonth and return result
            Console.WriteLine("Hello there");
            Console.WriteLine($"This is the example output for customer 1 on January 2018: {GetCustomerBillForSpecificMonth(2018, 1, 1)}");
        }
    }
}