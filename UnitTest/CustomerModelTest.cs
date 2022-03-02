using storeModel;
using Xunit;

namespace UnitTest
{
    public class CustomerModelTest
    {
        [Fact]
        public void ShouldHaveValidName()
        {
            //Arrange
            Customer _cust = new Customer();
            string validname = "Abdu";
            //Act
            _cust.CustName = validname;
            //Assert
            Assert.NotNull(_cust.CustName); 
            Assert.Equal(validname, _cust.CustName); 
        }


       
    }
}