using System.Text.RegularExpressions;

namespace storeModel
{
    public class Customer
    {
        public int CustID { get; set; }
        public string CustName { get; set; }
        public string CustAddress { get; set; }
        public string CustPhone  {get;set;}
        public string CustEmail { get; set; }
        // public List<Orders>  Orders {get;set;}
        public Customer()
        {
            CustID =0;
            CustName = " ";
            CustAddress = " ";
            CustPhone = "";
            CustEmail = " ";
            // CustOrders = new List<Orders>(){new Orders()};


        }

    }

}
