
namespace storeUI
{
    public enum MenuType
    {
        
        mainMenu,
        Exit,
        OrderHistory,
        AddCustomer,
        SearchCustomer,
        ViewInventory,
        PlaceOrder,
        SearchProduct,
        ListOfStore,
        ListOrders,
        LogIn,
        LogOut,
        Registor,
        ListOfCustomer,
        ReplenishInventory,
        StoreList
        // NewTech
    
    }
    public interface IMenu
    {
        /// <summary>
        /// Will display the menu the user Choise in the terminal 
        /// </summary>
        void Display();

        /// <summary>
        /// Will record the user's choice what they need
        /// </summary>
        /// <returns>Return the menu that will change your screen</returns>

        MenuType UserChoice();
    }
}