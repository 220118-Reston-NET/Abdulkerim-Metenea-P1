using storeBL;
using storeModel;
namespace storeUI
{
    public class OrderHistory : IMenu
    {
        private List<Orders> _ListOfOrders;
        private List<StoreFront> _ListStore;
        private List<Customer> _ListCustomer;
        private ICustomerBL _CustBL;
        private ILineItemsBL _LineItemBL;
        private IStoreFrontBL _IStoreBL;
        private IOrderBL _OrderBL;
        public OrderHistory(IOrderBL P_OrderBL, IStoreFrontBL p_storeId, ILineItemsBL p_item , ICustomerBL p_custBL)
        {
            _OrderBL = P_OrderBL;
            _IStoreBL = p_storeId;
            _LineItemBL = p_item;
            _CustBL = p_custBL;
           _ListCustomer = _CustBL.GetAllCustomer();
        }

       
        public void Display()
        {
            Console.WriteLine("...............................................");
            Console.WriteLine("             [1] Check OrderHistory By Customer ID");
            Console.WriteLine(".............................................");
            Console.WriteLine(".              [0] Go Back                  .");
            Console.WriteLine(".............................................");
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
                    int custId = Convert.ToInt32(Console.ReadLine());
                    while (_OrderBL.OrderHistoryByCustID(custId).All(p => p.CustID != custId))
                    {
                        Console.WriteLine("Customer Id Not Correct, Try Aign!");
                        Console.WriteLine("Enter Customer ID ");
                        custId = Convert.ToInt32(Console.ReadLine());
                    }
                    string name = _CustBL.GetAllCustomer().Find(p=>p.CustID == custId).CustName ;
                    Console.WriteLine("|||||||||||||||||||||||||||||");
                    Console.WriteLine($"||  Hi {name} Welcome Back    ||");
                    Console.WriteLine("||  Her Is Your Order History ||");
                    Console.WriteLine("||||||||||||||||||||||||||||||");
                    foreach (var orders in _OrderBL.OrderHistoryByCustID(custId))
                    {         
                        Console.WriteLine("Customer Id => " + orders.CustID);
                        Console.WriteLine("Order Id  => " + orders.OrderID);
                        Console.WriteLine("Store Id  => " + orders.StoreID);

                        int p_storeId = orders.StoreID;
                        _IStoreBL.GetAllProductByStoreId(p_storeId);

                        foreach (var item in _IStoreBL.GetAllProductByStoreId(p_storeId))
                        {   

                            Console.WriteLine("Product Id => " + item.ProductID);
                            Console.WriteLine("Product Name =>" + item.ProductName);
                            Console.WriteLine("Price =>" + "$" + item.Price + ".00");
                            int orderId = orders.OrderID;
                            _LineItemBL.GetAllLineItemsByOrderID(orderId);
                            foreach (var itemQauntity in _LineItemBL.GetAllLineItemsByOrderID(orderId))
                            {
                            Console.WriteLine("Quantity => " + itemQauntity.Quantity);
                                int TotalPrice = item.Price * itemQauntity.Quantity;
                            Console.WriteLine("Total Price => " + "$" + TotalPrice + ".00");
                            }
                            
                            Console.WriteLine("----------------------------");
                            
                        }


                            Console.WriteLine("|||||||||||||||||||||||||||||");
                    }

                    Console.WriteLine("please Enter to continue");
                    Console.ReadLine();
                    return MenuType.OrderHistory;

                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return MenuType.OrderHistory;
            }
        }
    }
}
