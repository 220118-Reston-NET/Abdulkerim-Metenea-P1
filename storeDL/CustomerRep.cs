using System.Data.SqlClient;
using storeModel;

namespace storeDL
{
    public class CustomerRepo : ICustomerRepo
    {
        private IInventoryRepo _iInventoryRepo;
        private readonly string _connectionString;
        public CustomerRepo(string p_connectionString)
        {
            _connectionString = p_connectionString;
        }
        
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
                        CustPhone = reader.GetString(3),
                        CustEmail = reader.GetString(4),
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
                        CustPhone = reader.GetString(3),
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
                        CustPhone = reader.GetString(3),
                        CustEmail = reader.GetString(4),

                    });
                }
            }

            return P_Cust;
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
                        OrderDate = reader.GetDateTime(4),
                        ShopingCart = GetLineItemsByOrderID(reader.GetInt32(0))
                    });
                }
            }
            return ListOfOrders;
        }
        public List<Orders> OrderHistoryByCustID(int P_CustID)
        {
            List<Orders> ListOfOrders = new List<Orders>();
            // List<LineItems> Cart = new List<LineItems>();
            
           
            string sqlQuery = @"select o.OrderID,o.CustID,o.StoreID, o.TotalPrice,o.OrderDate from Orders o inner join Customer c
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
                        ShopingCart = GetLineItemsByOrderID(reader.GetInt32(0))
                        
                       
                    });

                    // foreach (var order in ListOfOrders)
                    // {
                    //     foreach (var item in order.ShopingCart)
                    //     {
                    //         order.TotalPrice += _iInventoryRepo.SearchProduct(item.ProductID).First().Price * item.Quantity;
                    //     }
                    // }

                }

               
                
                return ListOfOrders;

            }

           
        }
        public Decimal ShopTotalPrice(List<LineItems> ShopingCart)
        {
            decimal _totalprice = 0m;
            foreach (var item in ShopingCart)
            {
                _totalprice += item.Quantity * _iInventoryRepo.GetAllProduct().Find(p => p.ProductID.Equals(item.ProductID)).Price;
            }
            return _totalprice;

        }
         
       
       

        public List<Orders> OrderHistoryByStoreId(int p_storeId)
        {
            List<Orders> _listOrders = new List<Orders>();

            LineItems items = new LineItems();
            decimal ItemTotalPrice = _iInventoryRepo.SearchProduct(items.ProductID).First().Price * items.Quantity;
            string sqlQuery = @"Select * from Orders Where StoreID=@StoreID Order By OrderDate DESC , TotalPrice DESC";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@StoreID", p_storeId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _listOrders.Add(new Orders
                    {
                        OrderID = reader.GetInt32(0),
                        CustID = reader.GetInt32(1),
                        StoreID = reader.GetInt32(2),
                        TotalPrice = ItemTotalPrice,
                        OrderDate = reader.GetDateTime(4)

                    });
                    
                }
                
                
            }
            return _listOrders;
         
        }
        public Orders PlaceOrder(Orders p_order)
        {
           
            string OrdersqlQuery = @"insert into Orders values(@CustID,@StoreID,@TotalPrice,@Orderdate); select scope_Identity();";
            string ItemsqlQuery = @"insert into LineItems values(@OrderID,@ProductID,@Quantity)";
            _iInventoryRepo= new InventoryRepo(_connectionString);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(OrdersqlQuery, con);
                command.Parameters.AddWithValue("@CustID", p_order.CustID);
                command.Parameters.AddWithValue("@StoreID", p_order.StoreID);
                command.Parameters.AddWithValue("@TotalPrice", p_order.TotalPrice);
                command.Parameters.AddWithValue("@OrderDate",DateTime.Now);
                int OrderID = Convert.ToInt32(command.ExecuteScalar());


                foreach (var item in p_order.ShopingCart)
                {
                    SqlCommand command2 = new SqlCommand(ItemsqlQuery, con);
                    command2.Parameters.AddWithValue("@OrderID", OrderID++);
                    command2.Parameters.AddWithValue("@ProductID", item.ProductID);
                    command2.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command2.ExecuteNonQuery();
                    _iInventoryRepo.SubtractQuantity(p_order);
                  
                }


            }
            return p_order;

        }
        //\//////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////              LlineItems                     ///////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////
        public List<LineItems> GetLineItemsByOrderID(int p_OrderID)
        {
            List<LineItems> ShopingCart = new List<LineItems>();
            string sqlQuery = @"Select ProductID , Quantity  from LineItems  Where OrderID = @OrderID";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@OrderID", p_OrderID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ShopingCart.Add(new LineItems()
                    {   
                        OrderID = p_OrderID,
                        ProductID = reader.GetInt32(0),
                        Quantity = reader.GetInt32(1)
                    });
                }
            }

            return ShopingCart;
        }
        public List<LineItems> GetAllineItems()
        {
            List<LineItems> ListItems = new List<LineItems>();
            string sqlQuery = @"select * from LineItems";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListItems.Add(new LineItems()
                    {
                        OrderID = reader.GetInt32(0),
                        ProductID = reader.GetInt32(1),
                        Quantity = reader.GetInt32(2)

                    });
                }
            }

            return ListItems;
        }
        public List<LineItems> ReduceQuantity(int productId, int quantity)
        {
            List<LineItems> ListOfineItems = new List<LineItems>();
            string sqlQuery = @" Update LineItems set Quantity = Quantity - @Quantity where ProductID = @ProductID";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("ProductID", productId);
                command.Parameters.AddWithValue("Quantity", quantity);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListOfineItems.Add(new LineItems()
                    {
                        OrderID = reader.GetInt32(0),
                        ProductID = reader.GetInt32(1),
                        Quantity = reader.GetInt32(2)

                    });
                }
            }

            return ListOfineItems;
        }

    }
}