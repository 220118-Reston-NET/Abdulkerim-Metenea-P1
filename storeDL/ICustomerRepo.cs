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
        Customer UpdateCustomer(Customer p_Cust);
        List<Orders> GetAllOrders();
        List<Orders> OrderHistoryByCustID(int P_CustID);
        List<Orders> OrderHistoryByStoreId(int p_storeId);
        void PlaceOrder(int p_custId, int p_storeId, Decimal p_totalprice, DateTime p_OrderDate, List<LineItems> p_cart);
    }
}