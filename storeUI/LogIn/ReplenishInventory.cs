using storeBL;
using storeModel;
namespace storeUI
{
    public class ReplenishInventory : IMenu
    {
        private static Inventory _newProduct = new Inventory();
        IStoreFrontBL _invetoryBL;
        public ReplenishInventory(IStoreFrontBL p_inventory)
        {
            _invetoryBL = p_inventory;
            

        }

         private int p_storeId ;
        private int p_productId;
        private int p_quantity;
        public void Display()
        {
            Console.WriteLine("Enter StoreId");
            p_storeId = Convert.ToInt32(Console.ReadLine());
            List<StoreFront> listOfStores = _invetoryBL.ViewStoreFront(p_storeId);
            List<Inventory> _inventory = _invetoryBL.GetAllInventoryBYStoreId(p_storeId);
            while(listOfStores.All(p => p.StoreID != p_storeId))
            {
                Console.WriteLine("Enter StoreId Again");
                p_storeId = Convert.ToInt32(Console.ReadLine());
                
            }
            Console.WriteLine("Enter Product Id");
            p_productId = Convert.ToInt32(Console.ReadLine());
           while (_inventory.All(p => p.ProductID != p_productId))
           {
                Console.WriteLine("Wrong Product Id");
                p_productId = Convert.ToInt32(Console.ReadLine());
           }
                Console.WriteLine("Add product quantity");
                Console.WriteLine("[1] To Add Quantity ");
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
                    Console.WriteLine("Please Add Quantity!");
                    p_quantity = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Log.Information("Adding Quantity \n" + _newProduct);
                        _invetoryBL.AddProductQuantity(p_storeId, p_productId, p_quantity);
                        Log.Information("Quantity added Successfully!");
                        
                    }
                    catch (System.Exception exc)
                    {
                        Log.Warning("Faild to Adding Quantity!");
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Press Enter to Continue");
                        Console.ReadLine();
                    }
                    Console.WriteLine("Press Enter to Continue");
                    Console.ReadLine();
                    return MenuType.ReplenishInventory;
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return MenuType.ReplenishInventory; ;
            }
        }
    }
}



