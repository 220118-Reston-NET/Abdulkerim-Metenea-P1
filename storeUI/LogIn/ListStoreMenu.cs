using storeModel;
using storeBL;
namespace storeUI
{
    public class ListStoreMenu : IMenu
    {

        private IStoreFrontBL _storeBL;
        private List<StoreFront> listStore;
        public ListStoreMenu(IStoreFrontBL p_store)
        {
            _storeBL = p_store;
            listStore = _storeBL.GetAllStoreFront();
        }
        public static int selectStoreID;

        public void Display()
        {   Console.WriteLine("WWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("Which Store Do You Want Got");
            Console.WriteLine("Please Choise By Store ID");
            Console.WriteLine("T                         T");
            Console.WriteLine("WWWWWWWWWWWWWWWWWWWWWWWW");
            foreach (var store in listStore)
            {

             Console.WriteLine(store);
             Console.WriteLine("::::::::::::::::::::");
             
            }
            
            Console.WriteLine("[1] <==Choice By Store ID");
            Console.WriteLine("[0] <== Go Back");
        }

        public MenuType UserChoice()
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    return MenuType.ListOfCustomer;
                case "1":
                    Console.WriteLine("Enter Store ID ");
                    int storeId = Convert.ToInt32(Console.ReadLine());
                    while (listStore.All(p => p.StoreID != storeId))
                    {
                        Console.WriteLine("Try Store ID Agian  ");
                        storeId = Convert.ToInt32(Console.ReadLine());
                    }
                    selectStoreID = storeId;
                    return MenuType.PlaceOrder;
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return MenuType.ListOfStore;
            }
        }
    }
}