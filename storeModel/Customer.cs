using System.Text.RegularExpressions;

namespace storeModel
{
    public class Customer
    {
        public int CustID { get; set; }
        private string _name;
        public string CustName
        {
            get { return _name; }
            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    _name = value;
                }
                else
                {
                    throw new Exception("Cannot have numbers in the name");
                }
            }
        }
        public string CustAddress { get; set; }
        private int _phone;
        public int CustPhone
        {
            get { return _phone; }
            set
            {
                if (value >= 0)
                {
                    _phone = value;
                }
                else
                {
                    throw new Exception("Cannot have Negative numbers in the Phone");
                }
            }
        }
        public string CustEmail { get; set; }
        public Customer()
        {
            CustID = 0;
            CustName = "Abdu";
            CustAddress = " ";
            CustPhone = 0;
            CustEmail = " ";

        }

    }

}

