using CloudCustomers.Api.Controllers;
using CloudCustomers.Api.Models;
using CloudCustomers.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Controllers
{
    public class TestUsersController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            //ARRANGE
            var mockUsersService = new Mock<IUsersService>();

            mockUsersService.Setup(service =>
                service.GetAllUsers())
                .ReturnsAsync(new List<User>()
                {
                    new()
                    {
                        Id = 1,
                        Name = "Roslava",
                        Address = new Address()
                        {
                            Street = "First Str.",
                            City = "Varna",
                            ZipCode = "1246"
                        },
                        Email = "testRosi@abv.bg"
                    }
                });
            var sut = new UsersController(mockUsersService.Object);

            //ACT
            var result = (OkObjectResult)await sut.Get();

            //ASSERT
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesUserServiceExactltOnce()
        {
            //ARRANGE
            var mockUsersService = new Mock<IUsersService>();
            
            mockUsersService.Setup(service =>
                service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUsersService.Object);

            //ACT
            var result = await sut.Get();

            //Assert
            mockUsersService.Verify(service =>
                service.GetAllUsers(), Times.Once());
        }


        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfUsers()
        {
            //ARRANGE
            var mockUsersService = new Mock<IUsersService>();

            mockUsersService.Setup(service =>
                service.GetAllUsers())
                .ReturnsAsync(new List<User>()
                {
                    new()
                    {
                        Id = 1,
                        Name = "Roslava", 
                        Address = new Address()
                        {
                            Street = "First Str.",
                            City = "Varna",
                            ZipCode = "1246"
                        },
                        Email = "testRosi@abv.bg"
                    }
                });

            var sut = new UsersController(mockUsersService.Object);

            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task Get_OnNoUsersFound_Returns404()
        {
            //ARRANGE
            var mockUsersService = new Mock<IUsersService>();

            mockUsersService.Setup(service =>
                service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUsersService.Object);

            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

    }
}