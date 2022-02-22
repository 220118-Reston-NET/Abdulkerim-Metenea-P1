using System.Data.SqlClient;
using storeModel;

namespace storeDL
{
    public class InventoryRepo : IInventoryRepo
    {
        private ICustomerRepo _custRepo;
        private readonly string _connectionString;
        public InventoryRepo(string p_connectionString)
        {
            _connectionString = p_connectionString;
        }
      
        public List<StoreFront> ViewStoreFrontOrders(string p_location)
        {
            List<StoreFront> listOfStoreFront = new List<StoreFront>();
            string sqlQuery = "select * from StoreFront Where StoreAddress = @StoreAddress";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@StoreAddress", p_location);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listOfStoreFront.Add(new StoreFront()
                    {
                        StoreID = reader.GetInt32(0),
                        StoreName = reader.GetString(1),
                        StoreAddress = reader.GetString(2),
                        Products = GetAllProductByStoreId(reader.GetInt32(0)),
                        Orders = _custRepo.OrderHistoryByStoreId(reader.GetInt32(0))
                    });
                }
            }

            return listOfStoreFront;
        }
        public List<StoreFront> ViewStoreLocation()
        {
            List<StoreFront> listOfStore = new List<StoreFront>();
            string sqlQuery = "select StoreID,StoreName,StoreAddress from StoreFront";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listOfStore.Add(new StoreFront()
                    {
                        StoreID = reader.GetInt32(0),
                        StoreName = reader.GetString(1),
                        StoreAddress = reader.GetString(2)
                        // Products = new List<Products>(reader.GetInt32(0)),
                        // Orders = new List<Orders>(reader.GetInt32(0))
                    });
                }
            }

            return listOfStore;
        }
        public StoreFront AddStoreFront(StoreFront p_store)
        {
            string sqlQuery = @"insert into StoreFront values(@StoreID,@StoreName, @StoreAddress)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@StoreID", p_store.StoreID);
                command.Parameters.AddWithValue("@StoreName", p_store.StoreName);
                command.Parameters.AddWithValue("@StoreAddress", p_store.StoreAddress);
                command.ExecuteNonQuery();
            }
            return p_store;
        }
        public List<StoreFront> GetAllStoreFront()
        {
            List<StoreFront> ListOfStores = new List<StoreFront>();
            string sqlQuery = @"select * from StoreFront ";
                            
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListOfStores.Add(new StoreFront()
                    {
                        StoreID = reader.GetInt32(0),
                        StoreName = reader.GetString(1),
                        StoreAddress = reader.GetString(2),
                        Products = new List<Products>(reader.GetInt32(0)),
                        Orders = new List<Orders>(reader.GetInt32(0))
                    });
                }
            }

            return ListOfStores;

        }
        //=====================================================================================
        ///                      Inventory
        /// /////////////////////////////////////////////////////////////////////////////////\\
        public List<Inventory> GetAllInventory()
        {
            List<Inventory> Inve = new List<Inventory>();
            string sqlQuery = @"select * from Inventory";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                   Inve.Add(new Inventory()
                    {
                        StoreID = reader.GetInt32(0),
                        ProductID = reader.GetInt32(1),
                        Quantity = reader.GetInt32(2)
                    });
                }
            }

            return Inve;
        }
        public List<Inventory> AddProductQuantity(int p_storeId, int p_productId, int p_quantity)
        {       List<Inventory> _addQuantity = new List<Inventory>();
                string sqlQuery = @"Update Inventory 
                            set Quantity = Quantity + @Quantity where StoreID =@StoreID and ProductID = @ProductID ";
               using(SqlConnection con = new SqlConnection(_connectionString))
               {
                   con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    command.Parameters.AddWithValue("@StoreID", p_storeId);
                    command.Parameters.AddWithValue("@ProductID", p_productId);
                    command.Parameters.AddWithValue("@Quantity", p_quantity);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _addQuantity.Add(new Inventory()
                        {
                                StoreID = reader.GetInt32(0),
                                ProductID = reader.GetInt32(1),
                                Quantity = reader.GetInt32(2)
                        });
                    }      
               }
                return _addQuantity;
        }
        public void SubtractQuantity(int p_storeId, int p_productId, int p_quantity)
        {
            string sqlQuery = @"Update Inventory 
                                set Quantity = Quantity - @Quantity 
                               WHERE  StoreID = @StoreID and ProductID = @ProductID ";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@StoreID", p_storeId);
                command.Parameters.AddWithValue("@ProductID", p_productId);
                command.Parameters.AddWithValue("@Quantity", p_quantity);
               command.ExecuteNonQuery();
            }
        }
        public void CartQuantity( int p_storeId, int p_productId ,int p_quantity)
         {   
            string sqlQuery= @"SELECT MAX(Quantity) - MIN(Quantity) AS 'Placed quantity' FROM Inventory   where StoreID = @StoreID and ProductID = @ProductID ";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
               SqlCommand command = new SqlCommand(sqlQuery, con);
               command.Parameters.AddWithValue("@StoreID", p_storeId);
                command.Parameters.AddWithValue("@ProductID", p_productId);
                command.Parameters.AddWithValue("@Quantity", p_quantity);
                SqlDataReader reader = command.ExecuteReader();
                command.ExecuteNonQuery();
            }
        }

        public List<Products> GetAllProductByStoreId(int p_storeId)
        {
            string sqlQuery =@"select DISTINCT  p.ProductID , p.ProductName ,p.Price ,p.Description ,p.Category 
                                from Products p , Inventory i 
                                Where p.ProductID  = i.ProductID 
                                and i.StoreID = @P_storeId";
            List<Products> listProduct = new List<Products>();
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery , con);
                command.Parameters.AddWithValue("@P_storeId" , p_storeId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listProduct.Add( new Products ()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Price  = reader.GetDecimal(2),
                        Description = reader.GetString(3),
                        Category = reader.GetString(4)
                    });  
                }
            }
            return listProduct;

        }
        //////////////////////////////////////////////////////////////////////////////////
        //////////////                 Products                    //////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        public List<Products> GetAllProduct()
        {
            List<Products> ListOfproducts = new List<Products>();
            string sqlQuery = @"select * from Products";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListOfproducts.Add(new Products()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Description = reader.GetString(3),
                        Category = reader.GetString(4)
                    });
                }
            }

            return ListOfproducts;
        }
        public List<Products> SearchProduct(int p_ProductID)
        {
            List<Products> listProduct = new List<Products>();
            string sqlQuery = @"Select * from Products where ProductID = @ProductID";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@ProductID", p_ProductID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listProduct.Add(new Products()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Description = reader.GetString(3),
                        Category = reader.GetString(4)
                    });
                }
            }

            return listProduct;
        }
        public Products AddProduct(Products p_product)
        {
            string sqlQuery = @"insert into Products 
                            values(@ProductName, @Price, @Descriptione, @Category)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@ProductName", p_product.ProductName);
                command.Parameters.AddWithValue("@Price", p_product.Price);
                command.Parameters.AddWithValue("@Descriptione", p_product.Description);
                command.Parameters.AddWithValue("@Category", p_product.Category);
                command.ExecuteNonQuery();
            }
            return p_product;
        }
        public Products UpdateProduct(Products p_product)
        {
            string sqlQuery = @"UPDATE StoreApp.dbo.Products SET ProductName=@ProductName, Price=@Price, Description=@Description, Category=@Category
                                 WHERE ProductID= @ProductID";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@ProductID", p_product.ProductID);
                command.Parameters.AddWithValue("@ProductName", p_product.ProductName);
                command.Parameters.AddWithValue("@Price", p_product.Price);
                command.Parameters.AddWithValue("@Descriptione", p_product.Description);
                command.Parameters.AddWithValue("@Category", p_product.Category);
                command.ExecuteNonQuery();
            }
            return p_product;
        }
        public List<Products> DeleteProductById(int productId)
        {
            List<Products> DeleteProduct = new List<Products>();
            string sqlQuery = @"DELETE FROM StoreApp.dbo.Products WHERE ProductID=@ProductID";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@ProductID", productId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DeleteProduct.Remove(new Products()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Description = reader.GetString(3),
                        Category = reader.GetString(4)
                    });
                    
                }
            }
            return DeleteProduct;
        }
        //\//////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////              LlineItems                     ///////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////
        public List<LineItems> GetLineItemsByOrderID(int p_OrderID)
        {
            List<LineItems> Items = new List<LineItems>();
            string sqlQuery = @"Select li.OrderID ,li.ProductID ,li.Quantity  from LineItems li inner join Orders o on li.OrderID= o.OrderID Where li.OrderID = @OrderID";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@OrderID", p_OrderID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Items.Add(new LineItems()
                    {
                        OrderID = reader.GetInt32(0),
                        ProductID = reader.GetInt32(1),
                        Quantity = reader.GetInt32(2),
                    });
                }
            }

            return Items;
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