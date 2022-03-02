using storeModel;

namespace storeDL
{
    public interface IUserRepo
    {
        User Registor(User p_Cust);
        List<User> GetAllUsers();
        List<User> Login(string p_username, string p_password);
        List<Manager> MangerLogin(int Id);
        List<Manager> GetAllmanager();
        // List<User> GetAllUsers();
    }
}