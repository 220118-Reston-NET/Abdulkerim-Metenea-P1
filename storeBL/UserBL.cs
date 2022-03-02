using storeDL;
using storeModel;
namespace storeBL
{
    public class UserBL : IUserBL
    {    
        private ICustomerRepo _custRepo;
        private IUserRepo _repo;
        public UserBL(IUserRepo p_repo)
        {
            _repo = p_repo;
        }
        public User Registor(User p_Cust)
        {
            List<User> listusers = _repo.GetAllUsers();

             if (listusers.All(p => p.Username != p_Cust.Username 
                                  && p.Password != p_Cust.Password ))
                                 
            {
                return _repo.Registor(p_Cust);
            }
            else
            {
                throw new Exception("Username Alrady Exist in The Database\n" + p_Cust.Username);
            }
            
        }
        // public User UpdateUsername(string p_phone)
        // {
        //     return _repo.UpdateUserInfo(p_phone);
        // }
        public List<User> Login(string p_username, string p_password)
        {
         return _repo.Login(p_username,p_password);
            
        }
        public List<Manager> MangerLogin(int Id)
        {
            return _repo.MangerLogin(Id);
        }

        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }
        public List<Manager> GetAllmanager()
        {
            return _repo.GetAllmanager();
        }
    }
}