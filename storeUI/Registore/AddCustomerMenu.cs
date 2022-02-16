using storeBL;
using storeModel;
namespace storeUI
{
    public class AddCustomerMenu : IMenu
    {   
        private static Customer _newCust = new Customer();
        private ICustomerBL _CustBL ;
        public AddCustomerMenu(ICustomerBL p_CustBL)
        {
            _CustBL = p_CustBL;

        }
        public void Display()
        {
            Console.WriteLine("Enter Customer information");
            Console.WriteLine("[3] Name - " + _newCust.CustName);
            Console.WriteLine("[4] Address - " + _newCust.CustAddress);
            Console.WriteLine("[5] Phone- " + _newCust.CustPhone);
            Console.WriteLine("[1] Save" );
            Console.WriteLine("[0] Go Back");
        }


        public MenuType UserChoice()
        {
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0":
                        return MenuType.mainMenu;
                    case "1":
                        try
                        {
                            Log.Information("Adding Customer \n" + _newCust);
                            _CustBL.AddCustomer(_newCust);
                            Log.Information("Customer adding Successfully!");
                        }
                        catch (System.Exception exc)
                        {
                            Log.Warning("Faild to Adding Customer!");
                            Console.WriteLine(exc.Message);
                            Console.WriteLine("Press Enter to Continue");
                            Console.ReadLine();
                        }
                        return MenuType.mainMenu;
                    case "3":
                        Console.WriteLine("Please enter Customer Name!");
                        _newCust.CustName = Console.ReadLine();
                        return MenuType.AddCustomer;
                    case "4":
                        Console.WriteLine("Please enter Customer Address!");
                        _newCust.CustAddress = Console.ReadLine();
                        return MenuType.AddCustomer;
                    case "5":
                        Console.WriteLine("Please enter Customer Phone!");
                        _newCust.CustPhone = Console.ReadLine();
                        return MenuType.AddCustomer;
                    default:
                        Console.WriteLine("Please input a valid response");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return MenuType.AddCustomer; ;
                }
        }
    }
}