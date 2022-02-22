
namespace storeModel
{
    public class Orders
    {   
        public int OrderID {get;set;}
        public int CustID { get; set; }
        public int StoreID { get; set; }
        public Decimal TotalPrice{get;set;}
        public DateTime OrderDate{get;set;}
        public List<LineItems> LineItems{get;set;}
        

        public Orders()
        {
            CustID = 0;
            StoreID = 0;
            TotalPrice = 0;
            OrderDate = DateTime.Now;
            LineItems = new List<LineItems>(){new LineItems() };
        }
        public override string ToString()
        {
            return $"OrderID: {OrderID}\nStoreID: {StoreID}\nCustID: {CustID}\nTotalPrice: {TotalPrice.ToString("0")}\nOrderDate:{OrderDate}";
        }
         
    }
    
}