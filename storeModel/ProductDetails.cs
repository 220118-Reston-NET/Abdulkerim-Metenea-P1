namespace storeModel
{
    public class ProductDetails
    {
        public String ProductName { get; set; }
        public Decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public ProductDetails()
        {
            ProductName = "";
            ProductPrice =0;
            Quantity =0;
        }
    }
}