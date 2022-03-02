using System.Text.RegularExpressions;
namespace storeModel
{
    public class Manager
    {
       public int ManagerID { get; set; }
        public string ManagerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Manager()
        {
            ManagerID= 0;
            ManagerName = "";
            Address = " ";
            Phone = " ";
            Email = " ";

        }

    }

}

