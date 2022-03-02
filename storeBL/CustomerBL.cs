using System;
using System.Collections.Generic;
using System.Linq;
using storeDL;
using storeModel;
namespace storeBL
{
    public class CustomerBL : ICustomerBL
    {
        //Dependency Injection Pattern
        private IInventoryRepo _invrepo;
        private ICustomerRepo _repo;
        public CustomerBL(ICustomerRepo p_repo  )
        {
            _repo = p_repo;
        }
        public List<Customer> GetAllCustomer()
        {
            return _repo.GetAllCustomer();
            
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
        
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        ///
        ///                               Orders
        /// \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public List<Orders> GetAllOrders()
        {
            return _repo.GetAllOrders();
        }
        public List<Orders> OrderHistoryByCustID(int p_CustID)
        {
           List<Orders> listOrders = _repo.OrderHistoryByCustID(p_CustID);
           return listOrders.FindAll(p=>p.CustID ==p_CustID).ToList();   
               
        }
        public List<Orders> OrderHistoryByStoreId(int p_storeId)
        {
           return _repo.OrderHistoryByStoreId(p_storeId);

        }
        public Orders PlaceOrder(Orders P_order)
        {
            return _repo.PlaceOrder(P_order); ;

        }
        //////////////////////////////////////////////////////////////////////////////////////
        /////////////                         LineItems                       ////////////////
        /// //////////////////////////////////////////////////////////////////////////////////
        public List<LineItems> GetAllLineItems()
        {
            return _repo.GetAllineItems();
        }
        public List<LineItems> GetLineItemsByOrderID(int p_OrderID)
        {
            List<LineItems> ItemList = _repo.GetLineItemsByOrderID(p_OrderID);
            return ItemList.FindAll(p => p.OrderID == p_OrderID).ToList();
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