using System.Text.RegularExpressions;

namespace storeModel
{
    public class CustomerDetail
    {
        public int CustID { get; set; }
        private string _name;
        public string CustName { get; set; }
    
        public string CustAddress { get; set; }
        private string _phone;
        public string CustPhone { get; set; }
        public string CustEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        // public List<Orders>  Orders {get;set;}
        public CustomerDetail()
        {
            CustID = 0;
            CustName = " ";
            CustAddress = " ";
            CustPhone = "";
            CustEmail = " ";
            Username = "";
            Password = "";
            // CustOrders = new List<Orders>(){new Orders()};


        }

    }

}

