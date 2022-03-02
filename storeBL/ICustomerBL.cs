using storeModel;
namespace storeBL
{ 
    public interface ICustomerBL
    {
        Customer AddCustomer(Customer P_Cust);
        List<Customer> GetAllCustomer();
        List<Customer>  SearchCustomer(string name);
        List<Customer> GetCustomerByID(int p_custId);
        ////////////////////////////////////////////////////////////////////////
        /////////////            Orders                 /////////////////////////
        ////////////////////////////////////////////////////////////////////////
        List<Orders> GetAllOrders();
        List<Orders> OrderHistoryByCustID(int P_CustID);
        List<Orders> OrderHistoryByStoreId(int p_storeId);
        Orders PlaceOrder(Orders P_order);
        // List<Orders> GetOrderDetailsByOrderId(int p_orderId);
        // void AddUser(User registorUser);
        ///////      LineItems     //////////////////
        /// /////////////////////////////////////////
        List<LineItems> GetAllLineItems();
        List<LineItems> GetLineItemsByOrderID(int p_OrderID);
        List<LineItems> ReduceQuantity(int productId, int quantity);

    }
}

