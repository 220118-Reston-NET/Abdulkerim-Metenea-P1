
namespace storeUI
{
    public enum MenuType
    {
        
        mainMenu,
        Exit,
        LogIn,
        LogOut,
        Registor
    
    }
    public interface IMenu
    {
        void Display();
        MenuType UserChoice();
    }
}