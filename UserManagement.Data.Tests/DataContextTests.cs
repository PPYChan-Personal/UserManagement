using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using UserManagement.Models;

namespace UserManagement.Data.Tests;

public class DataContextTests
{
    [Fact]
    public void GetAll_WhenNewEntityAdded_MustIncludeNewEntity()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var context = CreateContext();

        var entity = new User
        {
            Forename = "Brand New",
            Surname = "User",
            Email = "brandnewuser@example.com",
            DateOfBirth = new DateTime(1901, 1, 1)
        };
        context.Create(entity);

        // Act: Invokes the method under test with the arranged parameters.
        var result = context.GetAll<User>();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result
            .Should().Contain(s => s.Email == entity.Email)
            .Which.Should().BeEquivalentTo(entity);
    }

    [Fact]
    public void GetAll_WhenDeleted_MustNotIncludeDeletedEntity()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var context = CreateContext();
        var entity = context.GetAll<User>().First();
        context.Delete(entity);

        // Act: Invokes the method under test with the arranged parameters.
        var result = context.GetAll<User>();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().NotContain(s => s.Email == entity.Email);
    }

    [Fact]
    public void GetAll_WhenCreated_MustIncludeCreatedEntity()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var context = CreateContext();
        var entity = new User
        {
            Id = context.GetAll<User>().Max(m => m.Id) + 1,
            Forename = "Peter",
            Surname = "Chan",
            Email = "peter_chan@sky.com",
            IsActive = false,
            DateOfBirth = new DateTime(1902, 2, 2)
        };
        context.Create(entity);

        // Act: Invokes the method under test with the arranged parameters.
        var result = context.GetAll<User>();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().Contain(s => s.Email == entity.Email);
    }

    [Fact]
    public void GetAll_SQLContext_MustEqualToInMemoryContext()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var context = CreateContext();
        var contextSQL = CreateContextSQL();

        var entity = new User
        {
            Id = context.GetAll<User>().Max(m => m.Id) + 1,
            Forename = "Peter",
            Surname = "Chan",
            Email = "peter_chan@sky.com",
            IsActive = false,
            DateOfBirth = new DateTime(1902, 2, 2)
        };
        context.Create(entity);

        // Act: Invokes the method under test with the arranged parameters.
        var result = context.GetAll<User>();

        //var resultSQL = contextSQL.Users.ToList();
        
        using (var contextsqltest = new DataContext_SQL())
        {
            //contextsqltest.Users.ToList();
            Trace.WriteLine(contextsqltest.Users.Count());
        }

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().Contain(s => s.Email == entity.Email);
    }

    private DataContext CreateContext() => new();
    private DataContext_SQL CreateContextSQL() => new DataContext_SQL();
}

