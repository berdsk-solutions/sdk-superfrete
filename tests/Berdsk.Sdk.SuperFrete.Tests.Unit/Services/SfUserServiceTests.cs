using System.Net;
using Berdsk.Sdk.SuperFrete.Services.Users;
using Berdsk.Sdk.SuperFrete.Services.Users.Dtos;
using Berdsk.Sdk.SuperFrete.Tests.Unit.Helpers;
using FluentAssertions;

namespace Berdsk.Sdk.SuperFrete.Tests.Unit.Services
{
    public class SfUserServiceTests
    {
        private const string UserInfoJson =
            """
            {
              "id": "3kCKGqzlPFfCEj2X1Tkob07SN2M2",
              "firstname": "João",
              "lastname": "Silva",
              "phone": "+5511999999999",
              "email": "joao@gmail.com",
              "document": "12345678901",
              "limits": {
                "shipments": 3,
                "shipments_available": 2
              },
              "balance": 1763.04
            }
            """;

        [Fact]
        public async Task GetUserInfoAsync_ReturnsUserInfo()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, UserInfoJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfUserService(httpClient);

            // Act
            var result = await service.GetUserInfoAsync();

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be("3kCKGqzlPFfCEj2X1Tkob07SN2M2");
            result.Firstname.Should().Be("João");
            result.Lastname.Should().Be("Silva");
            result.Email.Should().Be("joao@gmail.com");
            result.Balance.Should().Be(1763.04m);
            result.Limits.Should().NotBeNull();
            result.Limits!.Shipments.Should().Be(3);
            result.Limits.ShipmentsAvailable.Should().Be(2);
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/user");
            handler.LastRequest.Method.Should().Be(HttpMethod.Get);
        }

        [Fact]
        public async Task GetAddressesAsync_ReturnsAddressList()
        {
            // Arrange
            const string addressesJson =
                """
                [
                  {
                    "id": "addr-001",
                    "postal_code": "01310100",
                    "address": "Rua Augusta",
                    "number": "100",
                    "complement": "Apto 42",
                    "district": "Consolação",
                    "city": "São Paulo",
                    "state_abbr": "SP"
                  },
                  {
                    "id": "addr-002",
                    "postal_code": "20040020",
                    "address": "Rua da Carioca",
                    "number": "50",
                    "complement": null,
                    "district": "Centro",
                    "city": "Rio de Janeiro",
                    "state_abbr": "RJ"
                  }
                ]
                """;
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, addressesJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfUserService(httpClient);

            // Act
            var result = await service.GetAddressesAsync();

            // Assert
            result.Should().NotBeNull();
            result!.Should().HaveCount(2);
            result[0].Id.Should().Be("addr-001");
            result[0].PostalCode.Should().Be("01310100");
            result[0].City.Should().Be("São Paulo");
            result[0].StateAbbr.Should().Be("SP");
            result[1].City.Should().Be("Rio de Janeiro");
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/user/addresses");
            handler.LastRequest.Method.Should().Be(HttpMethod.Get);
        }

        [Fact]
        public async Task GetUserInfoAsync_WhenUnauthorized_ThrowsSfUnauthorizedException()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.Unauthorized,
                """{"message":"Token inválido ou não fornecido","code":"UNAUTHORIZED"}""");
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfUserService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<SfUnauthorizedException>(
                () => service.GetUserInfoAsync());
        }
    }
}
