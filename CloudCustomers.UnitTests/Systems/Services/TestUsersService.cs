using CloudCustomers.Api.Models;
using CloudCustomers.Api.Models.Config;
using CloudCustomers.Api.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_OnInvoke_InvokesHttpGetRequest()
        {
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResorceList(expectedResponse);

            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var httpClient = new HttpClient(handlerMock.Object);

            var sut = new UsersService(httpClient, config);
            
            await sut.GetAllUsers();

            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>
                (req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
            
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);

            var result = await sut.GetAllUsers();

            result.Count.Should().Be(0);

        }


        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResorceList(expectedResponse, endpoint);

            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);

            var result = await sut.GetAllUsers();
            var uri = new Uri(endpoint);

            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>
                (req => req.Method == HttpMethod.Get
                && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
                );
        }
    }
}
