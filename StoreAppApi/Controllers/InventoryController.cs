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
// using Microsoft.Extensions.Caching.Memory;
using storeBL;
using storeModel;

namespace StoreAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IInventoryBL _invBL;
        private ICustomerBL _custBL;
        private IUserBL _verifyBL;

        public InventoryController(IInventoryBL p_invBL , ICustomerBL p_cust ,IUserBL p_verifyBL)
        {
            _invBL = p_invBL;
            _custBL = p_cust;
            _verifyBL = p_verifyBL;
        }
        [HttpGet("ViewStoreLocation")]
        public IActionResult  ViewStore()
        {
            
         try 
            {
               List<StoreFront> stores = _invBL.GetAllStoreFront();
               foreach (var item in stores)
               {
                    Log.Information("Store Location Viewd" + DateTime.Now);
                      
               }
                return Ok(_invBL.GetAllStoreFront());
            }
            catch (SqlException)
            {
                Log.Warning("Search Store location  faild!");
                return NotFound();
            }
        }
        [HttpGet("ViewOrder")]
        public IActionResult ViewStoreOrders(int storeId)
        {
            try
            {  
                OrderDetail _orderdetail = new OrderDetail();
                List<Orders> _listOrder = _custBL.GetAllOrders();
                List<StoreFront> _storeList = _invBL.GetAllStoreFront();
                List<Customer> _cust = _custBL.GetAllCustomer();

                Orders _order = _listOrder.Find(p => p.StoreID.Equals(storeId));
                if (_order == null)
                {
                    return BadRequest("Customer Not registord");
                }


                _order.StoreID = storeId;
                _orderdetail.OrderID = _listOrder.Find(p => p.StoreID.Equals(storeId)).OrderID;
                _orderdetail.CustName = _custBL.GetCustomerByID(storeId).Find(p => p.CustID.Equals(_order.CustID)).CustName;
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
                Log.Information(" Order Viewd By Store Location" + DateTime.Now);
                return Ok(_orderdetail);

            }
            catch (SqlException)
            {
                Log.Warning("store location Order Viewd fail!" + DateTime.Now);
                return NotFound();
            }
        }


        [HttpPost("Replenish")]
        public IActionResult AddProduct( int ManagerId,[FromBody] Products p_product)
        {
            try
            {  List<Manager> _listManager = _verifyBL.GetAllmanager();

                Manager _manager = _listManager.Find(p => p.ManagerID.Equals(ManagerId));

                if (_manager.ManagerID == ManagerId)
                {
                    
                    Log.Information("product Added" + DateTime.Now);
                    return Created("Product Added Successfully", _invBL.AddProduct(p_product));
                    

                }
                return BadRequest("Mnager Authorization needed");
               
            }
            catch (System.Exception ex)
            {
                Log.Warning("Product Adding faild" + DateTime.Now);
                return Conflict(ex.Message);
            }
        }

    }
}
