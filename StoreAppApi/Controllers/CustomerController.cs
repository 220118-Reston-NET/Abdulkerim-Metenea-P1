using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
// using StoreAppApi.DataTransferObject;
// using Microsoft.Extensions.Caching.Memory;
using storeBL;
using storeModel;

namespace StoreAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerBL _custBL;
        private IInventoryBL _invBL;
        private IUserBL _verifyBL;
        //   private IMemoryCache _memoryCache;
        public CustomerController(ICustomerBL p_custBL, IInventoryBL p_invBL, IUserBL p_verifyBL)
        {
            _custBL = p_custBL;
            _invBL = p_invBL;
            _verifyBL = p_verifyBL;
        }
        //   _memoryCache = p_memoryCache;
        // GET: api/Customer
        [HttpGet("GetAll")]
        // [Route("name")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Admin")]
        public IActionResult GetAllCustomer(int ManagerId)
        {
            List<Manager> _listManager = _verifyBL.GetAllmanager();

            Manager _manager = _listManager.Find(p => p.ManagerID.Equals(ManagerId));

            if (_manager.ManagerID == ManagerId)
            {

                return Ok(_custBL.GetAllCustomer());
            }

            return BadRequest("Only Manager can Access");


        }
        [HttpPost("Registor")]
        public IActionResult Registoration([FromBody] CustomerDetail p_cust)
        {
            try
            {
                // List<User> _listuser = _verifyBL.GetAllUsers();
                // User _user = _listuser.Find(p => p.Username.Equals(username));
                // User _user2 = _listuser.Find(p => p.Password.Equals(password));

                // if (_user.Username == username || _user2.Password == password)
                // {

                //     return BadRequest("You Alreday have Accunt please Please LogIn Correct information");
                // }
                CustomerDetail _customer = new CustomerDetail();
                Customer newCust = new Customer();
                newCust.CustName = p_cust.CustName;
                newCust.CustPhone = p_cust.CustPhone;
                newCust.CustEmail = p_cust.CustEmail;
                newCust.CustAddress = p_cust.CustAddress;

                _custBL.AddCustomer(newCust);
                User AddNewUser = new User();
                AddNewUser.Username = p_cust.Username;
                AddNewUser.Password = p_cust.Password;
                _verifyBL.Registor(AddNewUser);
               
                return Created("Registored Scuccessfully ", p_cust);

            }
            catch (System.Exception ex)
            {

                return Conflict(ex.Message);
            }
        }
        // GET: api/Customer/5
        [HttpGet]
        public IActionResult GetCustomerByName([FromQuery] string Name)
        {
            try
            {
                return Ok(_custBL.SearchCustomer(Name));
            }
            catch (SqlException)
            {
                return NotFound();
            }
        }
        // GET: OrderHistory
        [HttpGet("CustomerOrders")]
        public IActionResult OrderHistory(int custId, Menu OrderBy)
        {
            try
            {
                OrderDetail _orderdetail = new OrderDetail();
                List<Orders> _listOrder = _custBL.GetAllOrders();
                List<StoreFront> _storeList = _invBL.GetAllStoreFront();
                List<Customer> _cust = _custBL.GetAllCustomer();

                Orders _order = _listOrder.Find(p => p.CustID.Equals(custId));

                if (_order == null)
                {
                    return BadRequest("No Order found For Thsi customer ID");
                }


                // _order.CustID = custId;
                _orderdetail.OrderID = _listOrder.Find(p => p.CustID.Equals(custId)).OrderID;
                _orderdetail.CustName = _custBL.GetCustomerByID(custId).Find(p => p.CustID.Equals(_order.CustID)).CustName;
                _orderdetail.StoreName = _invBL.GetAllStoreFront().Find(p => p.StoreID.Equals(_order.StoreID)).StoreName;


                foreach (var item in _order.ShopingCart)
                {
                    _orderdetail.ShopingCart.Add(new ProductDetails
                    {
                        ProductName = _invBL.SearchProduct(item.ProductID).First().ProductName,
                        ProductPrice = _invBL.SearchProduct(item.ProductID).First().Price,
                        Quantity = item.Quantity

                    });

                }
                foreach (var item in _orderdetail.ShopingCart)
                {
                    _orderdetail.TotalPrice += item.ProductPrice * item.Quantity;
                }
                _orderdetail.Orderdate = _order.OrderDate;


                return Ok(_orderdetail);

            }
            catch (SqlException)
            {

                return NotFound();
            }
        }

        [HttpGet("OrderDetailes")]
        public IActionResult GetOrderDetailByOrderID(int ManagerId, [FromQuery] int orderId, Menu OrderBy)
        {

            List<Manager> _listManager = _verifyBL.GetAllmanager();

            Manager _manager = _listManager.Find(p => p.ManagerID.Equals(ManagerId));
            try
            {
                if (_manager.ManagerID != ManagerId)
                {

                    return BadRequest("Only Manager can Access");
                }
                OrderDetail _orderdetail = new OrderDetail();
                List<Orders> _listOrder = _custBL.GetAllOrders();
                List<StoreFront> _storeList = _invBL.GetAllStoreFront();
                List<Customer> _cust = _custBL.GetAllCustomer();

                Orders _order = _listOrder.Find(p => p.OrderID.Equals(orderId));
                if (_order == null)
                {
                    return BadRequest("No Order History!");
                }


                _orderdetail.OrderID = orderId;
                _orderdetail.CustName = _custBL.GetCustomerByID(orderId).Find(p => p.CustID.Equals(_order.CustID)).CustName;
                _orderdetail.StoreName = _invBL.GetAllStoreFront().Find(p => p.StoreID.Equals(_order.StoreID)).StoreName;
                foreach (var item in _order.ShopingCart)
                {
                    _orderdetail.ShopingCart.Add(new ProductDetails
                    {
                        ProductName = _invBL.SearchProduct(item.ProductID).First().ProductName,
                        ProductPrice = _invBL.SearchProduct(item.ProductID).First().Price,
                        Quantity = item.Quantity

                    });

                }
                foreach (var item in _orderdetail.ShopingCart)
                {
                    _orderdetail.TotalPrice += item.ProductPrice * item.Quantity;
                }
                _orderdetail.Orderdate = _order.OrderDate;
                Log.Information("Order detail viewed" + DateTime.Now);
                return Ok(_orderdetail);


            }
            catch (System.Exception)
            {
                Log.Warning("Order Searching  fail!" + DateTime.Now);
                return NotFound();

            }

        }


        // PUT: api/Customer/5
        [HttpPut("PlaceOrder")]
        public IActionResult PlaceOrder(string username, string password, [FromBody] Orders p_order)
        {
            try
            {    
                List<User> _listuser = _verifyBL.GetAllUsers();
                User _user = _listuser.Find(p => p.Username.Equals(username));
                User _user2 = _listuser.Find(p => p.Password.Equals(password));

                if (_user.Username != username && _user2.Password != password)
                {

                    return BadRequest("username or password not correct");
                }

                LineItems items = p_order.ShopingCart.Find(p => p.OrderID.Equals(p_order.OrderID));
                p_order.TotalPrice = _invBL.SearchProduct(items.ProductID).First().Price * items.Quantity;
                Log.Information("new Order placed" + DateTime.Now);
                return Created("Thank Your Order Placed successfully", _custBL.PlaceOrder(p_order));

            }
            catch (System.Exception ex)
            {
                Log.Warning("Placing Order fail!" + DateTime.Now);
                return Conflict(ex.Message);
            }

        }
    }
}
