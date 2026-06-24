using System.Net;
using Berdsk.Sdk.SuperFrete.Services.Cart;
using Berdsk.Sdk.SuperFrete.Services.Cart.Dtos;
using Berdsk.Sdk.SuperFrete.Tests.Unit.Helpers;
using FluentAssertions;

namespace Berdsk.Sdk.SuperFrete.Tests.Unit.Services
{
    public class SfCartServiceTests
    {
        private static SfAddToCartRequest BuildValidRequest() => new SfAddToCartRequest
        {
            Service = 1,
            Platform = "MinhaLoja",
            From = new SfCartSenderRequest
            {
                Name = "Remetente Teste",
                Document = "12345678901",
                Address = "Rua Augusta",
                Number = "100",
                District = "Consolação",
                City = "São Paulo",
                StateAbbr = "SP",
                PostalCode = "01310100"
            },
            To = new SfCartRecipientRequest
            {
                Name = "Destinatário Teste",
                Email = "destinatario@teste.com",
                Document = "98765432100",
                Address = "Rua da Carioca",
                Number = "50",
                District = "Centro",
                City = "Rio de Janeiro",
                StateAbbr = "RJ",
                PostalCode = "20040020"
            },
            Volumes = new SfCartVolumeRequest
            {
                Height = 5f,
                Width = 11f,
                Length = 16f,
                Weight = 0.3f
            }
        };

        [Fact]
        public async Task AddToCartAsync_ValidRequest_ReturnsCartResponse()
        {
            // Arrange
            const string responseJson = """{"id":"r8p3dhqjn4I0tBpnvkpU","price":34.27,"status":"pending"}""";
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, responseJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfCartService(httpClient);

            // Act
            var result = await service.AddToCartAsync(BuildValidRequest());

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be("r8p3dhqjn4I0tBpnvkpU");
            result.Price.Should().BeApproximately(34.27f, 0.01f);
            result.Status.Should().Be("pending");
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/cart");
            handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        }

        [Fact]
        public async Task AddToCartAsync_WhenBadRequest_ThrowsSfBadRequestException()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.BadRequest,
                """{"message":"Dados do remetente inválidos","code":"INVALID_SENDER"}""");
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfCartService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<SfBadRequestException>(
                () => service.AddToCartAsync(BuildValidRequest()));
        }

        [Fact]
        public async Task AddToCartAsync_WhenUnauthorized_ThrowsSfUnauthorizedException()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.Unauthorized,
                """{"message":"Token inválido ou não fornecido","code":"UNAUTHORIZED"}""");
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfCartService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<SfUnauthorizedException>(
                () => service.AddToCartAsync(BuildValidRequest()));
        }
    }
}
