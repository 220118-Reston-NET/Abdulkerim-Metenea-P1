using System.Collections.Generic;
using Moq;
using storeBL;
using storeDL;
using storeModel;
using Xunit;

namespace UnitTest;

public class CustomerBLTest
{
    [Fact]
    public void ShouldGetAllCustomer()
    {
        //Arrange
        string Name = "Abdu";
        int Phone = 1223034444;
        Customer _cust = new Customer()
        {
            CustName = Name,
            CustPhone = Phone
        };
        List<Customer> expectedListOfCustomer = new List<Customer>();
        expectedListOfCustomer.Add(_cust);

        Mock<ICustomerRepo> mockRepo = new Mock<ICustomerRepo>();

        mockRepo.Setup(repo=>repo.GetAllCustomer()).Returns(expectedListOfCustomer);
        ICustomerBL custBL = new CustomerBL(mockRepo.Object);
        //Act
        List<Customer> actualListOfCustomer = custBL.GetAllCustomer();
        Assert.Same(expectedListOfCustomer, actualListOfCustomer);
        Assert.Equal(Name ,actualListOfCustomer[0].CustName);
        Assert.Equal(Phone, actualListOfCustomer[0].CustPhone);
    
    }
    
    [Fact]
    public void ShouldGetAllOrders()
    {
        //Arrange
        int  orderId= 1;
        int storeId = 1;
        Orders _order = new Orders()
        {
            OrderID = orderId,
            StoreID = storeId
        };
        List<Orders> expectedListOfOrders = new List<Orders>();
        expectedListOfOrders.Add(_order);

        Mock<ICustomerRepo> mockRepo = new Mock<ICustomerRepo>();

        mockRepo.Setup(repo => repo.GetAllOrders()).Returns(expectedListOfOrders);
        ICustomerBL custBL = new CustomerBL(mockRepo.Object);
        //Act
        List<Orders> actualListOfOrders = custBL.GetAllOrders();
        Assert.Same(expectedListOfOrders, actualListOfOrders);
        Assert.Equal(orderId, actualListOfOrders[0].OrderID);
        Assert.Equal(storeId, actualListOfOrders[0].StoreID);

    }
    
}