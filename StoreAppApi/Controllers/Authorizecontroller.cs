using System;
using Dapper;
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
using storeDL;
using storeBL;
using storeModel;

namespace StoreAppApi.Controllers
{
    [Route("[controller]")]
    public class Authorizecontroller : Controller
    {
        private IUserBL _verifyBL;
        private ICustomerBL _custBL;

        public Authorizecontroller(IUserBL p_verifyBL ,ICustomerBL p_custBL)
        {
            _verifyBL = p_verifyBL;
            _custBL = p_custBL;
        }

        [HttpGet("Login")]
        public IActionResult login(string username, string Password)
        {
            List<User> _user = _verifyBL.Login(username,Password);
           try
           {
                if (_user.All(p=>p.Username ==username && p.Password==Password))
                {
                    Log.Information("User Loged in!" + DateTime.Now);
                    return Ok("Login Succsess :" + username);
                }
                 else
                 {
                     Log.Information("Log in Failed!");
                    return BadRequest("Login Failed");
                 }
                
                
               
           }
           catch (System.Exception)
           {    Log.Warning("User Login Failed" + DateTime.Now);
                return NotFound();
           }
            
        }

        [HttpPost("CreatAcount")]
        public IActionResult CreatAccount([FromBody] User p_cust)
        {
            try
            { 
                User AddNewUser = new User();
                AddNewUser.Username = p_cust.Username;
                AddNewUser.Password = p_cust.Password;
                _verifyBL.Registor(AddNewUser);
                Log.Information("user Created Account" + DateTime.Now);
                return Created("Scuccessfully Added : ", p_cust);

            }
            catch (System.Exception ex)
            {

                return Conflict(ex.Message);
            }
        }



    }
}