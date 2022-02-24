using storeModel;
using Xunit;

namespace UnitTest
{
    public class ManagerModelTest
    {
        [Theory]
        [InlineData("5Marta")]
        [InlineData("Abel33")]
        [InlineData("2Tomas")]
        public void NameNotContainNumber(string invalidName) // Should 
        {
            //Arrange
            Manager _manager = new Manager();
            //Act & Assert
            Assert.Throws<System.Exception>(()=>_manager.ManagName = invalidName);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void InvalidNegativePhoneNumber(int p_invalidPhone) // Should 
        {
            //Arrange
            Customer _cust = new Customer();
            //Act & Assert
            Assert.Throws<System.Exception>(
                () => _cust.CustPhone = p_invalidPhone
            );
        }
    }
}