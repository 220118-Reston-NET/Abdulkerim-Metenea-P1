namespace storeModel
{
    public class Inventory
    {
        public int StoreID { get; set; }
        public int ProductID { get; set; }
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
        public Inventory()
        {
            StoreID = 0;
            ProductID = 0;
            Quantity = 0;
        }


    }
}