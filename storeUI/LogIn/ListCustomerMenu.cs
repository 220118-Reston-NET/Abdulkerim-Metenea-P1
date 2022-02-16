using storeModel;
using storeBL;
namespace storeUI
{
    public class ListCustomerMenu : IMenu
    {

        private ICustomerBL _custBL;
       private List<Customer> listCustomer;
       public ListCustomerMenu(ICustomerBL  p_cust)
       {
           _custBL = p_cust;
           listCustomer = _custBL.GetAllCustomer();
       }
       public static int selectCustomerID;
       
        public void Display()
        {
            // Console.WriteLine("List of Our Customer");
            // foreach (var customer in listCustomer)
            // {
            //     Console.WriteLine(customer);
            // }
            Console.WriteLine("TTTTTTTTTTTTTTTTTTTTTTTTTT");
            Console.WriteLine("T       Hello!             T");
            Console.WriteLine("T    IF You're New         T");
            Console.WriteLine("T  Enter [9] To Registore  T");
            Console.WriteLine("T                          T");
            Console.WriteLine("TTTTTTTTTTTTTTTTTTTTTTTTTT");
            Console.WriteLine("T  IF You're a Customer    T");
            Console.WriteLine("T                          T");
            Console.WriteLine("T   [1] <== CustomerID     T");
            Console.WriteLine("T   [0] <== Go Back        T");
            Console.WriteLine("T .........................T");
        }

        public MenuType UserChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.mainMenu;
                case "1":
                    Console.WriteLine("Enter Customer ID ");
                    int CustId = Convert.ToInt32(Console.ReadLine());
                   while (listCustomer.All(p=>p.CustID != CustId))
                   {
                        Console.WriteLine("Try CustomerID Agian ");
                        CustId = Convert.ToInt32(Console.ReadLine());
                   }
                   selectCustomerID = CustId;
                   return MenuType.ListOfStore;
                
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return MenuType.ListOfCustomer;
            }
        }
    }
}