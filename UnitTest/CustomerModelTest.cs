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

        [Theory] 
        [InlineData("3bdu")]
        [InlineData("Abe5")]
        [InlineData("Tomas55")]
        public void NameNotContainNumber(string p_invalidName) // Should 
        {
            //Arrange
            Customer _cust = new Customer();
            //Act & Assert
            Assert.Throws<System.Exception>(
                () => _cust.CustName = p_invalidName
            );
        }

        [Theory] 
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void PhoneInvalidNegativeNumber(int p_invalidPhone) // Should 
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