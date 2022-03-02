using storeModel;
namespace storeBL
{
    public interface IUserBL
    {
        List<User> Login(string p_username, string p_password);
        User Registor(User p_Cust);
        List<Manager> MangerLogin(int Id);
        List<User> GetAllUsers();
        List<Manager> GetAllmanager();
        
    }
}
