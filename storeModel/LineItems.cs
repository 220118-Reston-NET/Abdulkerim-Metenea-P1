namespace storeModel
{
    public class LineItems 
    {
    public int OrderID { get; set; }
    public int ProductID {get;set;}
    private int _quantity;
    public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value >= 0)
                {
                    _quantity = value;
                }
                else
                {
                    throw new Exception("Quantity Should not be Negative");
                }
            }
        }
        public LineItems()
        {   
            OrderID = 0;
            ProductID = 0;
            Quantity = 0;
        }
       
        
    }

}