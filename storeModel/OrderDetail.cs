namespace storeModel
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public string CustName { get; set; }
        public string StoreName { get; set; }
        public List<ProductDetails> ShopingCart { get; set; }
        public Decimal TotalPrice { get; set; }
        public DateTime Orderdate { get; set; }
        public OrderDetail()
        {
            OrderID= 0;
            CustName ="";
            StoreName = "";
            ShopingCart = new List<ProductDetails>(){new ProductDetails()};
            TotalPrice =0;
            Orderdate = DateTime.Now;
        }
    }
}