// See https://aka.ms/new-console-template for more information
global using Serilog;
using Microsoft.Extensions.Configuration;
using storeBL;
using storeDL;
using storeUI;


//Creating and Configuring our Logger
Log.Logger = new LoggerConfiguration()
        .WriteTo.File("./logs/user.txt")
        .CreateLogger();


var configuration = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json")
  .Build();
string _connectionStrings = configuration.GetConnectionString("Reference2DB");


bool repeat = true;
IMenu menu = new MainMenu();

while (repeat)
{
    Console.Clear();
    menu.Display();
    MenuType ans = menu.UserChoice();
    switch (ans)
    {   
        case MenuType.ViewInventory:
            Log.Information("user can view storedetails");
            menu = new ViewInventoryMenu(new StoreFrontBL(new StoreFrontSQLRepo(_connectionStrings)),
                  new ProductsBL(new ProductSQLRepo(_connectionStrings)));
            break;
        case MenuType.OrderHistory:
                try
                {
                    Log.Information("Displaying Customer Order History Menu to user");
                    menu = new OrderHistory(new OrderBL(new OrderSQLRepo(_connectionStrings)),
                           new StoreFrontBL(new StoreFrontSQLRepo(_connectionStrings)),
                           new LineItemsBL(new LineItemSQLRepo(_connectionStrings)),
                           new CustomerBL(new CustSQLRepo(_connectionStrings)));
                }
                catch{
                    Console.WriteLine("customer doesn't exist");
                }  
            break; 
        case MenuType.SearchCustomer:
            Log.Information("Displaying searchCustomer Menu to user");
            menu = new SearchCustomerMenu(new CustomerBL(new CustSQLRepo(_connectionStrings)));
            break;
        case MenuType.AddCustomer:
            Log.Information("Displaying AddCustomer Menu to user");
            menu = new AddCustomerMenu(new CustomerBL(new CustSQLRepo(_connectionStrings)));
            break;
        case MenuType.ListOfCustomer:
            Log.Information("Displaying ListOf Customer Menu to user");
            menu = new ListCustomerMenu(new CustomerBL(new CustSQLRepo(_connectionStrings)));
            break;
        case MenuType.SearchProduct:
            Log.Information("Displaying All Products to user");
            menu = new StoreProductsMenu(new ProductsBL(new ProductSQLRepo(_connectionStrings)));
            break;
        case MenuType.ReplenishInventory:
            Log.Information("replenish");
            menu = new ReplenishInventory(new StoreFrontBL(new StoreFrontSQLRepo(_connectionStrings)));
            break;
        case MenuType.ListOfStore:
            Log.Information("Displaying All StoreFront ");
            menu = new ListStoreMenu(new StoreFrontBL(new StoreFrontSQLRepo(_connectionStrings)));
            break;
        case MenuType.PlaceOrder:
            Log.Information("Place Order");
            menu = new PlaceOrder(new StoreFrontBL(new StoreFrontSQLRepo(_connectionStrings)),new OrderBL(new OrderSQLRepo(_connectionStrings)));
            break;
        case MenuType.LogIn:
            Log.Information("Log In");
            menu = new ListCustomerMenu(new CustomerBL(new CustSQLRepo(_connectionStrings)));
             
            break;
        case MenuType.mainMenu:
            Log.Information("Displaying MainMenu to user");
            menu = new MainMenu();
            break;
        case MenuType.Exit:
            Log.Information("Exit Application");
            Log.CloseAndFlush();
            repeat = false;
            break;
        default:
            Console.WriteLine("Page does not exist!");
            Console.WriteLine("Press Enter To continue");
            Console.ReadLine();
            break;
    }
}