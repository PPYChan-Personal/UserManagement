using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests;

public class UserServiceTests
{
    [Fact]
    public void GetAll_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.GetAll();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeSameAs(users);
    }

    private IQueryable<User> SetupUsers(string forename = "Johnny", string surname = "User", string email = "juser@example.com", string dateofbirth = "02/01/1900", bool isActive = true)
    {
        var users = new[]
        {
            new User
            {
                Forename = forename,
                Surname = surname,
                Email = email,
                IsActive = isActive,
                DateOfBirth = dateofbirth
            }
        }.AsQueryable();

        _dataContext
            .Setup(s => s.GetAll<User>())
            .Returns(users);

        return users;
    }

    //[Fact]
    //public void GetAll_WhenContextReturnsEntities_MustReturnCreatedEntities()
    //{
    //    // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
    //    var service = CreateService();
    //    var users = CreateNewUser();

    //    service.Create(users.First().Forename, users.First().Surname, users.First().Email, users.First().DateOfBirth, users.First().IsActive);
    //    // Act: Invokes the method under test with the arranged parameters.
    //    var result = service.GetAll();

    //    // Assert: Verifies that the action of the method under test behaves as expected.
    //    result.Should().BeSameAs(users);

    //}
    //private IQueryable<User> CreateNewUser(string forename = "Johnny", string surname = "User", string email = "juser@example.com", string dateofbirth = "02/01/1900", bool isActive = true)
    //{
    //    var users = new[]
    //    {
    //        new User
    //        {
    //            Forename = forename,
    //            Surname = surname,
    //            Email = email,
    //            IsActive = isActive,
    //            DateOfBirth = dateofbirth
    //        }
    //    }.AsQueryable();

    //    _dataContext
    //        .Setup(s => s.GetAll<User>())
    //        .Returns(users);

    //    return users;
    //}


    private readonly Mock<IDataContext> _dataContext = new();
    private UserService CreateService() => new(_dataContext.Object);
}

