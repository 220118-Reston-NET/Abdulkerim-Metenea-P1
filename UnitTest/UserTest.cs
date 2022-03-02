using System.Collections.Generic;
using Moq;
using storeBL;
using storeDL;
using storeModel;
using Xunit;

namespace UnitTest;

public class UserBLTest
{
    [Fact]
    public void ShouldGetAllUserInfo()
    {
        //Arrange
        string UserName = "Abdu";
        string Password = "2233";
        User _cust = new User()
        {
            Username = UserName,
            Password = Password
        };
        List<User> expectedListOfUser = new List<User>();
        expectedListOfUser.Add(_cust);

        Mock<IUserRepo> mockRepo = new Mock<IUserRepo>();

        mockRepo.Setup(repo => repo.GetAllUsers()).Returns(expectedListOfUser);
        IUserBL custBL = new UserBL(mockRepo.Object);
        //Act
        List<User> actualListOfUser = custBL.GetAllUsers();
        Assert.Same(expectedListOfUser, actualListOfUser);
        Assert.Equal(UserName, actualListOfUser[0].Username);
        Assert.Equal(Password, actualListOfUser[0].Password);

    }

    
}