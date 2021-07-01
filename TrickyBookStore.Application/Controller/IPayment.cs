namespace TrickyBookStore.Application.Controller
{
    internal interface IPayment
    {
        double GetCustomerBillForSpecificMonth(int year, int month, long customerId);

        void StartProgram();
    }
}