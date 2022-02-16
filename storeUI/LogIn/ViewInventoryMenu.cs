using storeBL;
using storeModel;

namespace storeUI
{
    public class ViewInventoryMenu : IMenu
    {
        private List<StoreFront> _ListOfStores;
        private List<Inventory> _inventory;
        private List<Products> ListProduct;
        private IProductsBL _productBL;
        private IStoreFrontBL _storeBL;
        public ViewInventoryMenu(IStoreFrontBL P_StoreId ,IProductsBL p_proBL)
        {
            _storeBL = P_StoreId;
            _productBL = p_proBL;
            _ListOfStores = _storeBL.GetAllStoreFront();
            _inventory = _storeBL.GetAllInventory();
            ListProduct = _productBL.GetAllProduct();
        }
        // public static int SelectStoreId;
        public void Display()
        {   
            Console.WriteLine("Enter Store Information");
            Console.WriteLine("[1]===To Check By Store ID");
            Console.WriteLine("[0]===<<<Go Back");
        }

        public MenuType UserChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.mainMenu;
                case "1":
                        Console.WriteLine("please Enter Store ID");
                         int storeId = Convert.ToInt32(Console.ReadLine());
                   _ListOfStores = _storeBL.GetAllStoreFront();
                   _inventory = _storeBL.GetAllInventory();
                  
                    while (_ListOfStores.All(p => p.StoreID != storeId) && _inventory.All(p => p.Quantity > 1))
                    {
                        Console.WriteLine("StoreID Not Correct! Or This store out of stock");
                        Console.WriteLine("please Try Store ID Agin ");
                        storeId = Convert.ToInt32(Console.ReadLine());
                        
                    } 
                    foreach (var item in _ListOfStores)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine("WYWYWYWYYWWYWY");
                        storeId = _inventory.Find(p => p.StoreID == item.StoreID).StoreID;

                        foreach (var product in ListProduct)
                        {
                            Console.WriteLine("ProductId: "+product.ProductID);
                            Console.WriteLine("Product Name: " + product.ProductName);
                            Console.WriteLine("Price: " + "$"+product.Price+".00");
                            Console.WriteLine("Description: " + product.Description);
                            Console.WriteLine("Category: " + product.Category);
                            Console.WriteLine("Category: " + product.Category);
                            Console.WriteLine(":::::::::::::::::::::::::::::::");
    
                        }
                    }
                    Console.WriteLine("press Enter to Place Order");
                    Console.ReadLine();   
                    return MenuType.ListOfCustomer;  

                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return MenuType.ViewInventory;
            }

        }
    
    }
}