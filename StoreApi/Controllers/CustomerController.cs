using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.DataTransferObject;
// using Microsoft.Extensions.Caching.Memory;
using storeBL;
using storeModel;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerBL _custBL;
        //   private IMemoryCache _memoryCache;
        public CustomerController(ICustomerBL p_custBL)
        {
            _custBL = p_custBL;
        }
        //   _memoryCache = p_memoryCache;
        // GET: api/Customer
        [HttpGet("GetAll")]
        public IActionResult GetAllCustomer()
        {
            try
            {
                return Ok(_custBL.GetAllCustomer());
            }
            catch (SqlException)
            {
                return NotFound();
            }
        }

        // GET: api/Customer/5
        [HttpGet]
        public IActionResult GetCustomerByName([FromQuery]string Name)
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
        [HttpGet("{CustID}")]
        public IActionResult GetCustomerByID(int custId)
        {

            try
            {

                return Ok(_custBL.GetCustomerByID(custId));
            }
            catch (SqlException)
            {

                return NotFound();
            }
        }
        // GET: OrderHistory
        [HttpGet("GetOrderByCustID")]
        public IActionResult CustomerOrder(int custId)
        {   
            try
            {

                return Ok(_custBL.OrderHistoryByCustID(custId));
            }
            catch (SqlException)
            {

                return NotFound();
            }
        }
        
        // POST: api/Customer
        [HttpPost("Registor")]
        public IActionResult Registoration([FromBody] Registoration p_cust)
        {
            try
            {   
                Customer registor = new Customer();
                registor.CustName = p_cust.CustName;
                registor.CustPhone = p_cust.CustPhone;
                registor.CustEmail = p_cust.CustEmail;
                registor.CustAddress = p_cust.CustAddress;

                _custBL.AddCustomer(registor);
                UserVerification registorUser = new UserVerification();
                registorUser.UserName = p_cust.Username;
                registorUser.Password = p_cust.Password;
                _custBL.AddUser(registorUser);
                return Created("Scuccessfully Added",p_cust);

            }
            catch (System.Exception ex)
            {

                return Conflict(ex.Message);
            }
        }

        // PUT: api/Customer/5
        [HttpPut("Update/{id}")]
        public IActionResult Put(int id, [FromBody] Customer p_cust)
        {
            p_cust.CustID = id;
            try
            {
                return Ok(_custBL.UpdateCustomer(p_cust));

            }
            catch (System.Exception ex)
            {

                return Conflict(ex.Message);
            }

        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
