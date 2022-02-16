using storeModel;
using storeBL;
namespace storeUI
{
    public class PlaceOrder : IMenu
    {
        private IStoreFrontBL _storeBL;
        private IOrderBL _orderBL;
        private List<Products> Listproduct ;
        private List<Inventory> listInventory;
        public PlaceOrder(IStoreFrontBL p_store , IOrderBL p_order)
        {
            _orderBL = p_order;
            _storeBL = p_store;
            Listproduct = _storeBL.GetAllProductByStoreId(ListStoreMenu.selectStoreID);
            listInventory = _storeBL.GetAllInventoryBYStoreId(ListStoreMenu.selectStoreID);
        }
        private static List<LineItems> Cart = new List<LineItems>();
    

        public void Display()
        {
            Console.WriteLine("List Prodcuts From This Store =>"+ ListStoreMenu.selectStoreID);
            Console.WriteLine("..............................");
            int quantity = 0;
            foreach (var produt in Listproduct)
            {
                quantity = listInventory.Find(p=>p.ProductID == produt.ProductID).Quantity;
                Console.WriteLine("ProdductId :" + produt.ProductID);
                Console.WriteLine("Product name : " +produt.ProductName);
                Console.WriteLine("Price : "+ "$"+produt.Price+".00");
                Console.WriteLine("Description : "+ produt.Description);
                Console.WriteLine(" Catagory : "+ produt.Category);
                Console.WriteLine( "Quantity : "+ quantity);
                Console.WriteLine("TTTTTTTTTTTTTTTTTTT");
            }
            Console.WriteLine("W[2] == Choice Prorduct By ID");
            if (Cart.Count > 0)
            {
                Console.WriteLine("WWWWWWWWWWWWWWWWWWWWW");
                Console.WriteLine("Your cart Item");
                Console.WriteLine("...................");
                foreach (var item in Cart)
                {
                    Console.WriteLine(item);
                    Console.WriteLine(".................");
                    
                }
            }
            Console.WriteLine("W[0] <== Go Back");
            Console.WriteLine("W[9] <== Main Page");
            Console.WriteLine("W[1] == Check Out Your Order");
            Console.WriteLine("..................");
        }

        public MenuType UserChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    Cart = new List<LineItems>();
                    return MenuType.ListOfStore;
                case "2":
                    Console.WriteLine("Enter Product ID ");
                    int prodcutId = Convert.ToInt32(Console.ReadLine());
                    while (listInventory.All(p => p.ProductID != prodcutId))
                    {
                        Console.WriteLine("Try Agian product ID ");
                        prodcutId = Convert.ToInt32(Console.ReadLine());
                    }
                     Console.WriteLine("Enter Quantity");
                    string  quantity = Console.ReadLine();
                    while (!quantity.All(Char.IsDigit))
                    {
                        Console.WriteLine("try Quntity Agin");
                        quantity = Console.ReadLine();
                    }
                    int InvetoryQuantity = listInventory.Find(p=>p.ProductID == prodcutId).Quantity;
                    int Currentquantity = 0;
                    if (Cart.Count == 0)
                    {
                        Currentquantity = Convert.ToInt32(quantity);
                    }
                    else
                    {
                        Currentquantity = Convert.ToInt32(quantity) + Cart.Find(p => p.ProductID == prodcutId).Quantity;
                    }
                    
                    if (Currentquantity > InvetoryQuantity )
                    {
                        Console.WriteLine("Quantity More Than Inventory");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return MenuType.PlaceOrder;
                    }
                    if (Cart.All(p=>p.ProductID != prodcutId))
                    {  
                        Cart.Add(new LineItems()
                        {
                            ProductID = prodcutId,
                            Quantity = Convert.ToInt32(quantity)
                        
                        });
                    }
                    else
                    {
                        for (int i = 0; i < Cart.Count; i++)
                        {
                            if (Cart[i].ProductID == prodcutId)
                            {
                                Cart[i].Quantity += Convert.ToInt32(quantity);
                            }
                        }
                    }
                    Console.WriteLine("please Enter to Check Your Cart");
                    Console.ReadLine();
                    Console.WriteLine("please Enter to continue");
                    return MenuType.PlaceOrder;
                case "1":
                     _orderBL.PlaceOrder(ListCustomerMenu.selectCustomerID, ListStoreMenu.selectStoreID , TotalPrice(Cart) ,Cart);
                    Console.WriteLine("WWWWWWWWWWWWWWWWWWWWW");
                    Console.WriteLine(".......Thank You ......");
                    Console.WriteLine("    Order Succussfull");

                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return MenuType.PlaceOrder;
                    case "9":
                    return MenuType.mainMenu;
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return MenuType.PlaceOrder;
            }
        }
        public int TotalPrice( List<LineItems> cart)
        {
            int TotalPrice = 0;
            int ProducPrice = 0;
            foreach (var item in cart)
            {
                ProducPrice = Listproduct.Find(p=>p.ProductID == item.ProductID).Price;
                TotalPrice += ProducPrice * item.Quantity;
            }
            return TotalPrice;

        }
    }
}