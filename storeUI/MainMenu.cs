namespace storeUI
{
    //MainMenu inherits userMenu interface
    public class MainMenu : IMenu
    { 
        private ListCustomerMenu _cust;
        public void Display()
        {
            Console.WriteLine("..........................");
            Console.WriteLine(".  Welcome to Our Store!  .");
            Console.WriteLine("...........................");
            Console.WriteLine(".        LogIn            .");
            Console.WriteLine(".         [8]             .");
            Console.WriteLine("...........................");
            Console.WriteLine(".        Registor          .");
            Console.WriteLine(".         [9]             .");
            Console.WriteLine("...........................");




            
            string log = Console.ReadLine();
            if (log == "8")
            {  
               
                Console.WriteLine(".=========================.");
                Console.WriteLine(".                         .");
                Console.WriteLine(".   ReplenishInventory    .");
                Console.WriteLine(".         [6]             .");
                Console.WriteLine("...........................");
                Console.WriteLine(".      Place Order        .");
                Console.WriteLine(".         [5]             .");
                Console.WriteLine("...........................");
                Console.WriteLine(".     ViewInventory       .");
                Console.WriteLine(".         [4]             .");
                Console.WriteLine("...........................");
                Console.WriteLine(".    Check Order History  .");
                Console.WriteLine(".         [3]             .");
                Console.WriteLine("...........................");
                Console.WriteLine(".     Check Customer      .");
                Console.WriteLine(".         [2]             .");
                Console.WriteLine("...........................");
                Console.WriteLine(".       MainMenu          .");
                Console.WriteLine(".         [7]             .");
                Console.WriteLine("...........................");
                Console.WriteLine(".         EXit!           .");
                Console.WriteLine(".         [0]             .");
                Console.WriteLine("...........................");

            }
            if (log == "9")
            {
                Console.WriteLine("...........................");
                Console.WriteLine(".      Add customer       .");
                Console.WriteLine(".         [1]             .");
                Console.WriteLine("...........................");
                Console.WriteLine(".       MainMenu          .");
                Console.WriteLine(".         [7]             .");
                Console.WriteLine("...........................");
                Console.WriteLine(".         EXit!           .");
                Console.WriteLine(".         [0]             .");
                Console.WriteLine("...........................");
            }
        }
        public MenuType UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    return MenuType.Exit;
                case "1":
                    return MenuType.AddCustomer;
                case "2":
                    return MenuType.SearchCustomer;
                case "3":
                    return MenuType.OrderHistory;
                case "4":
                    return MenuType.ViewInventory;
                case "5":
                    return MenuType.ListOfCustomer;
                case "6":
                    return MenuType.ReplenishInventory;
                case "7":
                    return MenuType.mainMenu;
                case "8":
                    return MenuType.LogIn;
                case "9":
                    return MenuType.Registor;
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return MenuType.mainMenu;
            }
        }
    }
}
