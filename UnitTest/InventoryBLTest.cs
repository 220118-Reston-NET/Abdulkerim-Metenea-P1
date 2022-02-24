using System.Collections.Generic;
using storeModel;
using Moq;
using Xunit;
using storeDL;
using storeBL;

namespace UnitTest;

public class InventoryBLTest
{
    [Fact]
    public void ShouldGetAllStoreFront() //GEt All StoreFront Test
    {
        //Arrange
        string Name = "AllNeeds";
        string Address = "California";
        StoreFront _store= new StoreFront()
        {
            StoreName = Name,
            StoreAddress = Address
        };
        List<StoreFront> expectedListOfStore = new List<StoreFront>();
        expectedListOfStore.Add(_store);

        Mock<IInventoryRepo> mockRepo = new Mock<IInventoryRepo>();

        mockRepo.Setup(repo => repo.GetAllStoreFront()).Returns(expectedListOfStore);
        IInventoryBL custBL = new InventoryBL(mockRepo.Object);
        //Act
        List<StoreFront> actualListOfStore = custBL.GetAllStoreFront();
        Assert.Same(expectedListOfStore, actualListOfStore);
        Assert.Equal(Name, actualListOfStore[0].StoreName);
        Assert.Equal(Address, actualListOfStore[0].StoreAddress);

    }
    [Fact]
    public void ShouldGetAllProduct() //Get All Product Test
    {
        //Arrange
        string Name = "headphone";
        int productId = 3;
        Products _product = new Products()
        {
            ProductName = Name,
            ProductID = productId
        };
        List<Products> expectedListOfProducts = new List<Products>();
        expectedListOfProducts.Add(_product);

        Mock<IInventoryRepo> mockRepo = new Mock<IInventoryRepo>();

        mockRepo.Setup(repo => repo.GetAllProduct()).Returns(expectedListOfProducts);
        IInventoryBL custBL = new InventoryBL(mockRepo.Object);
        //Act
        List<Products> actualListOfProduct = custBL.GetAllProduct();
        Assert.Same(expectedListOfProducts, actualListOfProduct);
        Assert.Equal(Name, actualListOfProduct[0].ProductName);
        Assert.Equal(productId, actualListOfProduct[0].ProductID);

    }

}