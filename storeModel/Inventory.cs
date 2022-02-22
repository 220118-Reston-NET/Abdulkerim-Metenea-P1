namespace storeModel
{
        public class Inventory
        {
            public int StoreID { get; set; }
            public int ProductID { get; set; }
            public int Quantity { get; set; }
            public Inventory()
            {
                StoreID = 0;
                ProductID = 0;
                Quantity = 0;
            }
            public override string ToString()
            {
                return $"OrderID: {StoreID}\nProductID: {ProductID}\nQuantityt: {Quantity}";
            }

        }   
}