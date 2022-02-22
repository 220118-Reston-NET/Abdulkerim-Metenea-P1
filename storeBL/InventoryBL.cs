
using storeDL;
using storeModel;
namespace storeBL
{
    public class InventoryBL : IInventoryBL
    {
        private IInventoryRepo _repo;
        public InventoryBL(IInventoryRepo p_repo)
        {
            _repo = p_repo;

        }
        public List<StoreFront> GetAllStoreFront()
        {
            List<StoreFront> ListOfStores = _repo.GetAllStoreFront();
            return ListOfStores;
        }
        public StoreFront AddStoreFront(StoreFront p_store)
        {
            List<StoreFront> ListOfStoreFront = _repo.GetAllStoreFront();
            if (ListOfStoreFront.Contains(p_store) == false)
            {
                return _repo.AddStoreFront(p_store);
            }
            else
            {
                throw new Exception("Store Already Existe!");
            }

        }
        public List<StoreFront> ViewStoreLocation()
        {

            return _repo.ViewStoreLocation();
           
        }

        public List<StoreFront> ViewStoreFront(int P_StoreID)
        {
            List<StoreFront> ListOfStores = _repo.GetAllStoreFront();
            return ListOfStores
            .Where(Store => Store.StoreID == P_StoreID)
            .ToList();
        }
        public List<StoreFront> ViewStoreFrontOrders(string p_location)
        {
            return _repo.ViewStoreFrontOrders(p_location);
        }

        public List<Inventory> GetAllInventoryBYStoreId(int p_storeId)
        {
            List<Inventory> inventory = _repo.GetAllInventory();
            return inventory
            .Where(inv => inv.StoreID == p_storeId)
            .ToList();
        }

        public List<Inventory> GetAllInventory()
        {
            return _repo.GetAllInventory();
            
            
        }
        public List<Inventory> AddProductQuantity(int p_storeId, int p_productId, int p_quantity)
        {    
            return  _repo.AddProductQuantity(p_storeId ,p_productId ,p_quantity);
            
        }

        public void SubtractQuantity(int p_storeId, int p_productId, int p_quantity)
        {
          _repo.SubtractQuantity(p_storeId, p_productId, p_quantity);
        }

        //||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        ///                       Products
        ///////////////////////////////////////////////////////////////////////////////////
        public List<Products> GetAllProduct()
        {
            List<Products> ListOfProducts = _repo.GetAllProduct();
            return ListOfProducts;
        }
        public Products AddProduct(Products p_product)
        {
            List<Products> listOfProduct = _repo.GetAllProduct();
            if (listOfProduct.Count < 10)
            {
                return _repo.AddProduct(p_product);
            }
            else
            {
                Console.WriteLine("10 produc limited!");
            }
            return p_product;
        }
        public List<Products> SearchProduct(int p_productId)
        {
            List<Products> Product = _repo.SearchProduct(p_productId);
            return Product
            .Where(p => p.ProductID == p_productId)
            .ToList();
        }
        public List<Products> GetAllProductByStoreId(int p_storeId)
        {
            return _repo.GetAllProductByStoreId(p_storeId);
        }
        public Products UpdateProduct(Products p_product)
        {
            return  _repo.UpdateProduct(p_product);
            
        }
        
        public List<Products> DeleteProductById(int productId)
        {
            List<Products> listPorduct = _repo.DeleteProductById(productId);
            return listPorduct.Where(p => p.ProductID == productId).ToList();
        }
        //////////////////////////////////////////////////////////////////////////////////////
        /////////////                         LineItems                       ////////////////
        /// //////////////////////////////////////////////////////////////////////////////////
        public List<LineItems> GetAllineItems()
        {
            return _repo.GetAllineItems();
        }
        public List<LineItems> GetLineItemsByOrderID(int p_OrderID)
        {
            List<LineItems> ItemList = _repo.GetLineItemsByOrderID(p_OrderID);
            return ItemList.Where(p => p.OrderID == p_OrderID).ToList();
        }

        public List<LineItems> ReduceQuantity(int productId, int quantity)
        {
            List<LineItems> updatedQuantity = _repo.ReduceQuantity(productId, quantity);
            return updatedQuantity
            .Where(p => p.ProductID == productId && p.Quantity == quantity)
            .ToList();
        }

        
    }
}