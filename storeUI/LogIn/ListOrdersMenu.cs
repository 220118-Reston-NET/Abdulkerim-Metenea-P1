using storeModel;
using storeBL;
namespace storeUI
{
    public class ListOrdersMenu : IMenu
    {

        private IOrderBL _orderBL;
        private List<Orders> listOrders;
        public ListOrdersMenu(IOrderBL p_order)
        {
            _orderBL = p_order;
            listOrders = _orderBL.GetAllOrders();
        }
        public static int selectOrderID;

        public void Display()
        {
            Console.WriteLine("List of Our Customer");
            foreach (var orders in listOrders)
            {
                Console.WriteLine(orders);
            }
            Console.WriteLine("[1] <== OrderID");
            Console.WriteLine("[0] <== Go Back");
        }

        public MenuType UserChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.mainMenu;
                case "1":
                    Console.WriteLine("Enter Order ID ");
                    int orderId = Convert.ToInt32(Console.ReadLine());
                    while (listOrders.All(p => p.OrderID != orderId))
                    {
                        Console.WriteLine("Try OrderID Agian ");
                        orderId = Convert.ToInt32(Console.ReadLine());
                    }
                    selectOrderID = orderId;
                    return MenuType.ListOrders;
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return MenuType.ListOrders;
            }
        }
    }
}