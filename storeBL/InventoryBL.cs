
using storeDL;
using storeModel;
namespace storeBL
{
    public class InventoryBL : IInventoryBL
    {
        private IInventoryRepo _invetoryRepo;
        public InventoryBL(IInventoryRepo p_repo)
        {
            _invetoryRepo = p_repo;

        }
        public List<StoreFront> GetAllStoreFront()
        {
            List<StoreFront> ListOfStores = _invetoryRepo.GetAllStoreFront();
            return ListOfStores;
        }
        public StoreFront AddStoreFront(StoreFront p_store)
        {
            List<StoreFront> ListOfStoreFront = _invetoryRepo.GetAllStoreFront();
            if (ListOfStoreFront.Contains(p_store) == false)
            {
                return _invetoryRepo.AddStoreFront(p_store);
            }
            else
            {
                throw new Exception("Store Already Existe!");
            }

        }
        public List<StoreFront> ViewStoreLocation()
        {

            return _invetoryRepo.ViewStoreLocation();
           
        }

        public List<StoreFront> ViewStoreFront(int P_StoreID)
        {
            List<StoreFront> ListOfStores =_invetoryRepo.GetAllStoreFront();
            return ListOfStores
            .Where(Store => Store.StoreID == P_StoreID)
            .ToList();
        }
        public List<StoreFront> ViewStoreFrontOrders(string p_location)
        {
            return _invetoryRepo.ViewStoreFrontOrders(p_location);
        }

        public List<Inventory> GetAllInventoryBYStoreId(int p_storeId)
        {
            List<Inventory> inventory = _invetoryRepo.GetAllInventory();
            return inventory
            .Where(inv => inv.StoreID == p_storeId)
            .ToList();
        }

        public List<Inventory> GetAllInventory()
        {
            return _invetoryRepo.GetAllInventory();
            
            
        }
        public List<Inventory> AddProductQuantity(int p_storeId, int p_productId, int p_quantity)
        {    
            return _invetoryRepo.AddProductQuantity(p_storeId ,p_productId ,p_quantity);
            
        }

        public void SubtractQuantity(int p_storeId, int p_productId, int p_quantity)
        {
          _invetoryRepo.SubtractQuantity(p_storeId, p_productId, p_quantity);
        }

        //||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        ///                       Products
        ///////////////////////////////////////////////////////////////////////////////////
        public List<Products> GetAllProduct()
        {
            List<Products> ListOfProducts = _invetoryRepo.GetAllProduct();
            return ListOfProducts;
        }
        public Products AddProduct(Products p_product)
        {
            List<Products> listOfProduct = _invetoryRepo.GetAllProduct();
            if (listOfProduct.Count < 10)
            {
                return _invetoryRepo.AddProduct(p_product);
            }
            else
            {
                Console.WriteLine("10 produc limited!");
            }
            return p_product;
        }
        public List<Products> SearchProduct(int p_productId)
        {
            List<Products> Product = _invetoryRepo.SearchProduct(p_productId);
            return Product
            .Where(p => p.ProductID == p_productId)
            .ToList();
        }
        public List<Products> GetAllProductByStoreId(int p_storeId)
        {
            return _invetoryRepo.GetAllProductByStoreId(p_storeId);
        }
        public Products UpdateProduct(Products p_product)
        {
            return  _invetoryRepo.UpdateProduct(p_product);
            
        }
        
        public List<Products> DeleteProductById(int productId)
        {
            List<Products> listPorduct = _invetoryRepo.DeleteProductById(productId);
            return listPorduct.Where(p => p.ProductID == productId).ToList();
        }
        //////////////////////////////////////////////////////////////////////////////////////
        /////////////                         LineItems                       ////////////////
        /// //////////////////////////////////////////////////////////////////////////////////
        public List<LineItems> GetAllineItems()
        {
            return _invetoryRepo.GetAllineItems();
        }
        public List<LineItems> GetLineItemsByOrderID(int p_OrderID)
        {
            List<LineItems> ItemList = _invetoryRepo.GetLineItemsByOrderID(p_OrderID);
            return ItemList.Where(p => p.OrderID == p_OrderID).ToList();
        }

        public List<LineItems> ReduceQuantity(int productId, int quantity)
        {
            List<LineItems> updatedQuantity = _invetoryRepo.ReduceQuantity(productId, quantity);
            return updatedQuantity
            .Where(p => p.ProductID == productId && p.Quantity == quantity)
            .ToList();
        }

        
    }
}