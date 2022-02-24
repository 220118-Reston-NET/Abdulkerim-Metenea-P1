
using storeModel;
namespace StoreApi.DataTransferObject
{
    public class Registoration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CustName { get; set; }
        public string CustAddress { get; set; }
        public int CustPhone { get; set; }
        public string CustEmail { get; set; }
    }
}