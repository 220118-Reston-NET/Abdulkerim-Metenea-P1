using storeBL;
using storeModel;
namespace storeUI
{
    public class SearchCustomerMenu : IMenu
    {
        private List<Customer> _ListOfCustomer;
        private ICustomerBL _CustBL;
        public SearchCustomerMenu(ICustomerBL p_CustBL)
        {
            _CustBL = p_CustBL;
        }

        public void Display()
        {
            Console.WriteLine("Select an Options!");
            Console.WriteLine("[2] Check by Name");
            Console.WriteLine("[0] Go Back");
        }
        public MenuType UserChoice()
        {
              string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "0":
                        return MenuType.mainMenu;
                    case "2":
                        Console.WriteLine("please Enter Customer Name");
                        string name = Console.ReadLine();
                        List<Customer> listOfCustomer = _CustBL.SearchCustomer(name);

                        foreach (var item in listOfCustomer)
                        {
                            Console.WriteLine("************");
                            Console.WriteLine(item);
                        }
                        Console.ReadLine();
                        Console.WriteLine("Press Enter to Continue");
                        
                        return MenuType.mainMenu;
                    default:
                        Console.WriteLine("Please input a valid response");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return MenuType.SearchCustomer;
                }
        }
    }
}
