using storeModel;
using storeBL;
using Xunit;
using System.Collections.Generic;
using Moq;
using storeDL;

namespace UnitTest
{
    public class LineItemModelTest
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-90)]
        [InlineData(-1567)]
        [InlineData(-11)]
        [InlineData(-1567)]
        [InlineData(-9)]
        [InlineData(-00222)]
        [InlineData(-11283)]
        public void invalidQuantity(int p_quantity) // Should 
        {
            //Arrange
            LineItems _cust = new LineItems();
            //Act & Assert
            Assert.Throws<System.Exception>(() => _cust.Quantity = p_quantity
            );
        }
        public void ShouldGetAllItems() //Get All Items Test
        {
            //Arrange
            int ProductID = 1;
            int OrderID= 1;
            LineItems _Items = new LineItems()
            {
                ProductID = ProductID,
                OrderID = OrderID
            };
            List<LineItems> expectedListOfLineItems = new List<LineItems>();
            expectedListOfLineItems.Add(_Items);

            Mock<ICustomerRepo> mockRepo = new Mock<ICustomerRepo>();

            mockRepo.Setup(repo => repo.GetAllineItems()).Returns(expectedListOfLineItems);
            ICustomerBL custBL = new CustomerBL(mockRepo.Object);
            //Act
            List<LineItems> actualListOfItems = custBL.GetAllLineItems();
            Assert.Same(expectedListOfLineItems, actualListOfItems);
            Assert.Equal(ProductID, actualListOfItems[0].ProductID);
            Assert.Equal(OrderID, actualListOfItems[0].OrderID);

        }

    }
}