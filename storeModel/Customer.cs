namespace storeModel
{
    public class Customer
    {   public int CustID {get;set;}
        public string CustName { get; set; }
        public string CustAddress { get; set; }
        public int CustPhone { get; set; }
        public string CustEmail { get; set; }
         public List<Orders> Orders { get; set; }
        public Customer()
        {   
            CustID = 0;
            CustName = " ";
            CustAddress = " " ;
            CustPhone = 0;
            CustEmail = " ";
            Orders = new List<Orders>(){new Orders() };
        }
        public override string ToString()
        {
            return $"ID: {CustID}\nName: {CustName}\nAddress: {CustAddress}\nPhoneNumber: {CustPhone}\nEmail : {CustEmail}";
        }
    }

}

