namespace storeModel
{
    public class LineItems 
    {
        public int OrderID { get; set; }
        public int ProductID {get;set;}
        public int Quantity { get; set; }
        public LineItems()
        {   
            OrderID = 0;
            ProductID = 0;
            Quantity = 0;
        }
        public override string ToString()
        {
            return $"OrderID: {OrderID}\nProductID: {ProductID}\nQuantityt: {Quantity}";
        }
        
    }

}