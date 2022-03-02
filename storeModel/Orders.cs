
namespace storeModel
{
    public class Orders
    {
        public int OrderID { get; set; }
        public int CustID { get; set; }
        public int StoreID { get; set; }
        public Decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public List<LineItems> ShopingCart { get; set; }


        public Orders()
        {
            CustID = 0;
            StoreID = 0;
            TotalPrice = 0;
            OrderDate = DateTime.Now;
            ShopingCart = new List<LineItems>() { new LineItems() };
        }


    }

}