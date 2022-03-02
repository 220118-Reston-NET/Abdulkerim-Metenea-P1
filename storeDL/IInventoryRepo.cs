using storeModel;

namespace storeDL
{
    public interface IInventoryRepo
    {
        StoreFront AddStoreFront(StoreFront p_store);
        List<StoreFront> GetAllStoreFront();
        List<StoreFront> ViewStoreLocation();
        List<StoreFront> ViewStoreFrontOrders(string p_location);
        List<Inventory> GetAllInventory();
        List<Inventory> AddProductQuantity(int p_storeId, int p_productId, int p_quantity);
        void SubtractQuantity(Orders p_order);
        List<Products> GetAllProductByStoreId(int p_storeId);
        ////////////             Products /////////////////////////
        ////////////////////////////////////////////////////////////////
        List<Products> GetAllProduct();
        Products AddProduct(Products p_product);
        List<Products> SearchProduct(int p_productId);
        Products UpdateProduct(Products p_product);
      
    }

}
