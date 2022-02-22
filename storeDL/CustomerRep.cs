using System.Data.SqlClient;
using storeModel;

namespace storeDL
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly string _connectionString;
        public CustomerRepo(string p_connectionString)
        {
            _connectionString = p_connectionString;
        }
        private InventoryRepo _storeRepo;
        public Customer AddCustomer(Customer p_Cust)
        {
            string sqlQuery = @"insert into Customer 
                            values(@CustName, @CustAddress, @CustPhone, @CustEmail)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@CustName", p_Cust.CustName);
                command.Parameters.AddWithValue("@CustAddress", p_Cust.CustAddress);
                command.Parameters.AddWithValue("@CustPhone", p_Cust.CustPhone);
                command.Parameters.AddWithValue("@CustEmail", p_Cust.CustEmail);

                command.ExecuteNonQuery();
            }

            return p_Cust;
        }

        public List<Customer> GetAllCustomer()
        {
            List<Customer> ListOfCustomer = new List<Customer>();
            string sqlQuery = @"select * from Customer";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListOfCustomer.Add(new Customer()
                    {
                        CustID = reader.GetInt32(0),
                        CustName = reader.GetString(1),
                        CustAddress = reader.GetString(2),
                        CustPhone = reader.GetInt32(3),
                        CustEmail = reader.GetString(4)
                    });
                }
            }

            return ListOfCustomer;
        }

        public List<Customer> SearchCustomer(string name)
        {
            List<Customer> _Cust = new List<Customer>();
            string sqlQuery = @"Select * From Customer Where CustName = @CustName";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@CustName", name);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _Cust.Add(new Customer()
                    {
                        CustID = reader.GetInt32(0),
                        CustName = reader.GetString(1),
                        CustAddress = reader.GetString(2),
                        CustPhone = reader.GetInt32(3),
                        CustEmail = reader.GetString(4)

                    });
                }
            }

            return _Cust;
        }
        public List<Customer> GetCustomerByID(int p_custId)
        {
            List<Customer> P_Cust = new List<Customer>();
            string sqlQuery = @"Select * From Customer Where CustID = @CustID";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@CustID", p_custId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    P_Cust.Add(new Customer()
                    {
                        CustID = reader.GetInt32(0),
                        CustName = reader.GetString(1),
                        CustAddress = reader.GetString(2),
                        CustPhone = reader.GetInt32(3),
                        CustEmail = reader.GetString(4),
                        Orders = OrderHistoryByCustID(reader.GetInt32(0))

                    });
                }
            }

            return P_Cust;
        }
        public Customer UpdateCustomer(Customer p_Cust)
        {
            string sqlQuery = @"UPDATE StoreApp.dbo.Customer SET CustName= @CustName, CustAddress=@CustAddress,CustPhone=@CustPhone, CustEmail=@CustEmail  WHERE CustID= @CustID";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@CustID", p_Cust.CustID);
                command.Parameters.AddWithValue("@CustName", p_Cust.CustName);
                command.Parameters.AddWithValue("@CustAddress", p_Cust.CustAddress);
                command.Parameters.AddWithValue("@CustPhone", p_Cust.CustPhone);
                command.Parameters.AddWithValue("@CustEmail", p_Cust.CustEmail);

                command.ExecuteNonQuery();
            }

            return p_Cust;
        }

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        //                             Orders
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public List<Orders> GetAllOrders()
        {
            List<Orders> ListOfOrders = new List<Orders>();
            string sqlQuery = @"select * from Orders";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListOfOrders.Add(new Orders()
                    {
                        OrderID = reader.GetInt32(0),
                        CustID = reader.GetInt32(1),
                        StoreID = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                        OrderDate = reader.GetDateTime(4)
                    });
                }
            }
            return ListOfOrders;
        }
        public List<Orders> OrderHistoryByCustID(int P_CustID)
        {
            List<Orders> ListOfOrders = new List<Orders>();
               
            string sqlQuery = @"select o.OrderID,o.CustID,o.StoreID,o.TotalPrice,o.OrderDate from Orders o inner join Customer c
                             on o.CustID = c.CustID inner join LineItems li on o.OrderID = li.OrderID where o.CustID = @CustID Order By OrderDate ASC";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@CustID", P_CustID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    ListOfOrders.Add(new Orders()
                    {
                        OrderID = reader.GetInt32(0),
                        CustID = reader.GetInt32(1),
                        StoreID = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                        OrderDate = reader.GetDateTime(4),

                    });
                   
                    

                }
                // foreach (var item in ListOfOrders)
                // {
                //     int orderId = item.OrderID;
                //     item.LineItems = _storeRepo.GetLineItemsByOrderID(orderId);
                // };
            }
            return ListOfOrders;
        }
        public List<Orders> OrderHistoryByStoreId(int p_storeId)
        {
            List<Orders> ListOrders = new List<Orders>();
            string sqlQuery = @"Select * from Orders Where StoreID=@StoreID Order By OrderDate DESC , TotalPrice DESC";
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery,con);
                command.Parameters.AddWithValue("@StoreID",p_storeId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListOrders.Add(new Orders
                    {
                        OrderID = reader.GetInt32(0),
                        CustID = reader.GetInt32(1),
                        StoreID = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                        OrderDate = reader.GetDateTime(4)
                        
                    });
                }
            }
            return ListOrders;
        }
        

        public void PlaceOrder(int p_custId, int p_storeId, Decimal p_totalprice, DateTime p_Orderdate, List<LineItems> p_cart)
        {

            string OrdersqlQuery = @"insert into Orders values(@CustID,@StoreID,@TotalPrice,@OrderDate); select scope_Identity();";
            string ItemsqlQuery = @"insert into LineItems values(@OrderID,@ProductID,@Quantity)";
            _storeRepo = new InventoryRepo(_connectionString);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(OrdersqlQuery, con);
                command.Parameters.AddWithValue("@CustID", p_custId);
                command.Parameters.AddWithValue("@StoreID", p_storeId);
                command.Parameters.AddWithValue("@TotalPrice", p_totalprice);
                command.Parameters.AddWithValue("@OrderDate", p_Orderdate);
                int OrderID = Convert.ToInt32(command.ExecuteScalar());


                foreach (var item in p_cart)
                {
                    SqlCommand command2 = new SqlCommand(ItemsqlQuery, con);
                    command2.Parameters.AddWithValue("@OrderID", OrderID);
                    command2.Parameters.AddWithValue("@ProductID", item.ProductID);
                    command2.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command2.ExecuteNonQuery();
                    _storeRepo.SubtractQuantity(p_storeId, item.ProductID, item.Quantity);

                }


            }

        }

    }
}