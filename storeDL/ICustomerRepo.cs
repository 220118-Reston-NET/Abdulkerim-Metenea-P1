using storeModel;

namespace storeDL
{
    /// <summary>
    /// Data layer Responsible interacting with store database and doing CRUD operation
    /// C-creat, R-Read, U-Update,D-Delete
    /// </summary>
    public interface ICustomerRepo
    {
        Customer AddCustomer(Customer p_Cust);
        List<Customer> SearchCustomer(string name);
        List<Customer> GetCustomerByID(int p_custId);
        List<Customer> GetAllCustomer();
        List<Orders> GetAllOrders();
        List<Orders> OrderHistoryByCustID(int P_CustID);
        List<Orders> OrderHistoryByStoreId(int p_storeId);
        Orders PlaceOrder(Orders P_order);
        //////////    LineItems ///////////////////
        ////////////////////////////////////////////////
        List<LineItems> GetLineItemsByOrderID(int p_OrderID);
        List<LineItems> GetAllineItems();
        List<LineItems> ReduceQuantity(int productId, int quantity);
    }
}