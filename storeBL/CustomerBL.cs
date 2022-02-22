
// using System.Linq;
using storeDL;
using storeModel;
namespace storeBL
{
    public class CustomerBL : ICustomerBL
    {
        //Dependency Injection Pattern
        private ICustomerRepo _repo;
        public CustomerBL(ICustomerRepo p_repo )
        {
            _repo = p_repo;
        }
        public List<Customer> GetAllCustomer()
        {
            List<Customer> ListOfCustomer = _repo.GetAllCustomer();
            return ListOfCustomer;
        }
        public Customer AddCustomer(Customer p_Cust)
        {
            List<Customer> ListOfCustomer = _repo.GetAllCustomer();
            if (ListOfCustomer.All(p=>p.CustPhone !=p_Cust.CustPhone ))
            {
                return _repo.AddCustomer(p_Cust);
            }
            else
            {
                throw new Exception("Customer Alrady registord By This Phone Number:\n" + p_Cust.CustPhone);
            }
            
        }
        public List<Customer> SearchCustomer(String name)
        {
            List<Customer> listOfCustomer = _repo.GetAllCustomer(); 
            return listOfCustomer
            .Where(p => p.CustName.Contains(name))
            .ToList();
        }
        public List<Customer> GetCustomerByID(int p_custId)
        {
            List<Customer> listcustomer = _repo.GetCustomerByID(p_custId);
            return listcustomer.Where(p=>p.CustID == p_custId).ToList();
        }
        public Customer UpdateCustomer(Customer p_Cust)
        {
            return _repo.UpdateCustomer(p_Cust);
        }
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        ///
        ///                               Orders
        /// \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public List<Orders> GetAllOrders()
        {
            List<Orders> ListOfOrders = _repo.GetAllOrders();
            return ListOfOrders;
        }
        public List<Orders> OrderHistoryByCustID(int p_CustID)
        {
            List<Orders> CustomerOrder = _repo.OrderHistoryByCustID(p_CustID);
            return CustomerOrder.Where(p => p.CustID == p_CustID).ToList();       
        }
        public List<Orders> OrderHistoryByStoreId(int p_storeId)
        {
            List<Orders> storeOrderlist = _repo.OrderHistoryByStoreId(p_storeId);
            return storeOrderlist.Where(p=>p.StoreID==p_storeId).ToList();
        }

        public void PlaceOrder(int p_custId, int p_storeId, Decimal p_totalprice, DateTime p_OrderDate, List<LineItems> p_cart)
        {
            _repo.PlaceOrder(p_custId, p_storeId, p_totalprice, p_OrderDate, p_cart);

        }
    }
}