using System.Data.SqlClient;
using storeModel;

namespace storeDL
{
    public class UserRepo : IUserRepo
    {
        private readonly string _connectionString;
        public UserRepo(string p_connectionString)
        {
            _connectionString = p_connectionString;
        }
        public List<User> GetAllUsers()
        {
            List<User> ListUser = new List<User>();
            string sqlQuery = @"select * From UserVerify";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListUser.Add(new User()
                    {

                        Username = reader.GetString(0),
                        Password = reader.GetString(1)

                    });
                }
            }

            return ListUser;
        }
        public List<Manager> GetAllmanager()
        {
            List<Manager> manager = new List<Manager>();
            string sqlQuery = @"select * From Manager";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                   
                        manager.Add(new Manager()
                    {
                        ManagerID = reader.GetInt32(0),
                        ManagerName = reader.GetString(1),
                        Address = reader.GetString(2),
                        Phone = reader.GetString(3),
                        Email = reader.GetString(4)

                    });

                
                }
            }

            return manager;
        }
        public User Registor(User p_Cust)
        {
            string sqlQuery = @"insert into UserVerify 
                            values(@Username, @Password)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@Username", p_Cust.Username);
                command.Parameters.AddWithValue("@Password", p_Cust.Password);

                command.ExecuteNonQuery();
            }
            return p_Cust;
        }
        public List<User> Login(string p_username, string p_password)
        {
            List<User> _user = new List<User>();
            string sqlQuery = @"Select * from UserVerify  Where Username= @Username and Password=@Password";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@Username", p_username);
                command.Parameters.AddWithValue("@Password", p_password);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _user.Add(new User()
                    {
                        Username = reader.GetString(0),
                        Password = reader.GetString(1)
                    });
                }
            }
            return _user;
        }
        public List<Manager> MangerLogin(int Id)
        {
            List<Manager> manager = new List<Manager>();
            string sqlQuery = @"Select * from Manager  Where ManagerID= @ManagerID ";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@ManagerID", Id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    manager.Add(new Manager()
                    {
                        ManagerID = reader.GetInt32(0),
                        ManagerName = reader.GetString(1),
                        Address = reader.GetString(2),
                        Phone = reader.GetString(3),
                        Email = reader.GetString(4)
                       
                    });
                }
            }
            return manager;
        }


    }
}