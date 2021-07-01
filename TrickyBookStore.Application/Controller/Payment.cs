using System;
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
            int month, year;
            long customerId;
            bool rerun = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to The Tricky BookStore.");
                Console.WriteLine("Customer ID please!");
                try
                {
                    customerId = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Incorrect customer ID. Use default customer ID: 1");
                    customerId = 1;
                }
                Console.WriteLine("Month please!");
                try
                {
                    month = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Incorrect month. Use default month: 1");
                    month = 1;
                }
                Console.WriteLine("Year please!");
                try
                {
                    year = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Incorrect year. Use default year: 2018");
                    year = 2018;
                }
                Console.WriteLine($"The payment amount for customer {customerId} on {month}/{year}:");
                double sum = GetCustomerBillForSpecificMonth(year, month, customerId);
                Console.WriteLine(sum);
                Console.WriteLine("Press r to rerun!");
                rerun = Console.ReadLine() == "r";
            }
            while (rerun);
        }
    }
}