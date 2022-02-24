using storeModel;
namespace storeBL
{ 
    public interface ICustomerBL
    {
        Customer AddCustomer(Customer P_Cust);
        List<Customer> GetAllCustomer();
        List<Customer>  SearchCustomer(string name);
        List<Customer> GetCustomerByID(int p_custId);
        Customer UpdateCustomer(Customer p_Cust);
        ////////////////////////////////////////////////////////////////////////
        /////////////            Orders                 /////////////////////////
        ////////////////////////////////////////////////////////////////////////
        List<Orders> GetAllOrders();
        List<Orders> OrderHistoryByCustID(int P_CustID);
        List<Orders> OrderHistoryByStoreId(int p_storeId);
        void PlaceOrder(int p_custId, int p_storeId, Decimal p_totalPrice, DateTime p_OrderDate, List<LineItems> p_cart);
        void AddUser(UserVerification registorUser);
    }
}

