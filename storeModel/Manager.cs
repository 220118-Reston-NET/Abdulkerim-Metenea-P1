using System.Text.RegularExpressions;
namespace storeModel
{
    public class Manager
    {
        private string _name;
        public string ManagName
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
        public string Address { get; set; }
        private int _phone;
        public int Phone
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
        public string Email { get; set; }

        public Manager()
        {
            ManagName = "Abel";
            Address = " ";
            Phone = 0;
            Email = " ";

        }
        public override string ToString()
        {
            return $"Name: {ManagName }\nAddress: {Address}\nPhoneNumber: {Phone}\nEmail : {Email}";
        }
    }

}

