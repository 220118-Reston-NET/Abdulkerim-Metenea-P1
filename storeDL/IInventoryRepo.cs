using storeModel;

namespace storeDL
{
    /// <summary>
    /// Data layer Responsible interacting with store database and doing CRUD operation
    /// C-creat, R-Read, U-Update,D-Delete
    /// </summary>
    public interface IInventoryRepo
    {
        StoreFront AddStoreFront(StoreFront p_store);
        List<StoreFront> GetAllStoreFront();
        List<StoreFront> ViewStoreLocation();
        List<StoreFront> ViewStoreFrontOrders(string p_location);
        List<Inventory> GetAllInventory();
        List<Inventory> AddProductQuantity(int p_storeId, int p_productId, int p_quantity);
        void SubtractQuantity(int p_storeId, int p_productId, int p_quantity);
        List<Products> GetAllProductByStoreId(int p_storeId);
        ////////////             Products /////////////////////////
        ////////////////////////////////////////////////////////////////
        List<Products> GetAllProduct();
        Products AddProduct(Products p_product);
        List<Products> SearchProduct(int p_productId);
        Products UpdateProduct(Products p_product);
        List<Products> DeleteProductById(int productId);
        //////////    LineItems ///////////////////
        ////////////////////////////////////////////////
        List<LineItems> GetLineItemsByOrderID(int p_OrderID);
        List<LineItems> GetAllineItems();
        List<LineItems> ReduceQuantity(int productId, int quantity);
    }

}
