using storeBL;
using storeModel;
namespace storeUI
{
    public class StoreProductsMenu : IMenu
    {
        private List<Products> _products;
        private List<LineItems> _itemList;
        private IProductsBL _productBL;
        public StoreProductsMenu(IProductsBL P_name)
        {
            _productBL = P_name;
            
        }

        public void Display()
        {
            Console.WriteLine("*Welcome To Check our Products!****");
            Console.WriteLine("[2]<===To Check Produc List");
            Console.WriteLine("[1]<===Mainmenu");
            Console.WriteLine("[0]<<<Go Back");
        }

        public MenuType UserChoice()
        {
            string UserInput = Console.ReadLine();
            switch (UserInput)
            {
                case "0":
                return MenuType.ViewInventory;
                case "1":
                return MenuType.mainMenu;
                case "2":
                    
                    List<Products> Product = _productBL.GetAllProduct();
                    foreach (var items in Product)

                        {
                            Console.WriteLine($"*****Produc List**");
                            Console.WriteLine(items);

                        }   
                        
                        Console.WriteLine(" Press Enter To Place Order");
                        Console.ReadLine();
                        return MenuType.PlaceOrder;
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return MenuType.SearchProduct;
            }
            
        }
    }
}