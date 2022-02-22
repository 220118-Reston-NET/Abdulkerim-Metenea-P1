using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Caching.Memory;
using storeBL;
using storeModel;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IInventoryBL _invBL;
        private ICustomerBL _custBL;

        public InventoryController(IInventoryBL p_invBL , ICustomerBL p_cust)
        {
            _invBL = p_invBL;
            _custBL = p_cust;
        }
        [HttpGet("StoreAddress")]
        public IActionResult ViewStoreLocation()
        {
            try
            {
                return Ok(_invBL.ViewStoreLocation());
            }
            catch (SqlException)
            {

                return NotFound();
            }
        }
        [HttpGet("OrderByStoreLocation")]
        public IActionResult OrderByStorelocation(string location)
        {
        //   List<Orders> order = new List<Orders>();
        //    IEnumerable<Orders> sortedOrder = from Orders in order
        //     orderby Orders.OrderDate ascending, Orders.TotalPrice ascending
        //     select Orders;
            return Ok(_invBL.ViewStoreFrontOrders(location));
        }
        // public IEnumerable<string> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }
        // POST: api/Inventory
        [HttpPost("AddProduct")]
        public IActionResult Post([FromBody] Products p_product)
        {
            try
            {
                return Created("Product Added Successfully", _invBL.AddProduct(p_product));
            }
            catch (System.Exception ex)
            {

                return Conflict(ex.Message);
            }
        }


        // PUT: api/Inventory/5
        [HttpPut("UpdateProduct{id}")]
        public IActionResult Put(int id, [FromBody] Products p_product)
        {
            p_product.ProductID = id;
            try
            {
                return Ok(_invBL.UpdateProduct(p_product));
            }
            catch (System.Exception ex)
            {

                return Conflict(ex.Message);
            }
        }

        // DELETE: api/Inventory/5
        [HttpDelete("DeleteProduct{id}")]
        public IActionResult Delete(int ProductId)
        {
            try
            {
                return Ok(_invBL.DeleteProductById(ProductId));
            }
            catch (System.Exception ex)
            {

                return Conflict(ex.Message);
            }
        }
    }
}
