using storeModel;
namespace storeBL
{
    public interface IInventoryBL
    {
        List<StoreFront> ViewStoreFront(int P_StoreID);
        List<StoreFront> ViewStoreLocation();
        List<StoreFront> GetAllStoreFront();
        StoreFront AddStoreFront(StoreFront p_store);
        List<StoreFront> ViewStoreFrontOrders(string p_location);
        List<Inventory> GetAllInventoryBYStoreId(int p_storeId);
        List<Inventory> GetAllInventory();
        List<Inventory> AddProductQuantity(int p_storeId, int p_productId, int p_quantity);
        void SubtractQuantity(int p_storeId, int p_productId, int p_quantity);
        List<Products> GetAllProductByStoreId(int p_storeId);
        //||||||||||//////////////////////////////////////////
        //            Products
        ////////////////////////////////////////////////////
        List<Products> GetAllProduct();
        Products AddProduct(Products p_product);
        List<Products> SearchProduct(int p_productId);
        Products UpdateProduct(Products p_product);
        List<Products> DeleteProductById(int productId);
        ///////      LineItems     //////////////////
        /// /////////////////////////////////////////
        List<LineItems> GetAllineItems();
        List<LineItems> GetLineItemsByOrderID(int p_OrderID);
        List<LineItems> ReduceQuantity(int productId, int quantity);
        
    }
}