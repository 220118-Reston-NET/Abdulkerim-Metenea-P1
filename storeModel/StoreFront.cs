
namespace storeModel
{
    public class StoreFront
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public String StoreAddress { get; set; }
        public List<Products> Products { get; set; }
        public List<Orders> Orders { get; set; }
        public StoreFront()
        {   
            StoreID = 0;
            StoreName = "";
            StoreAddress = "";
            Products = new List<Products>() { new Products() };
            Orders = new List<Orders>() { new Orders() };
        }



    }

}