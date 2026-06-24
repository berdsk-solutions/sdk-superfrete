using System.Net;
using Berdsk.Sdk.SuperFrete.Services.Checkout;
using Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos;
using Berdsk.Sdk.SuperFrete.Tests.Unit.Helpers;
using FluentAssertions;

namespace Berdsk.Sdk.SuperFrete.Tests.Unit.Services
{
    public class SfCheckoutServiceTests
    {
        private const string ValidResponseJson =
            """
            {
              "purchase": {
                "subtotal": 33.33,
                "discount": 1.11,
                "total": 32.22
              },
              "orders": [
                {
                  "id": "r8p3dhqjn4I0tBpnvkpU",
                  "status": "released"
                }
              ]
            }
            """;

        [Fact]
        public async Task FinalizeOrderAsync_ValidOrders_ReturnsCheckoutResponse()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, ValidResponseJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfCheckoutService(httpClient);

            var request = new SfCheckoutRequest
            {
                Orders = new[] { "r8p3dhqjn4I0tBpnvkpU" }
            };

            // Act
            var result = await service.FinalizeOrderAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.Purchase.Should().NotBeNull();
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/checkout");
            handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        }

        [Fact]
        public async Task FinalizeOrderAsync_WhenBadRequest_ThrowsSfBadRequestException()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.BadRequest,
                """{"message":"Etiqueta não encontrada no carrinho","code":"ORDER_NOT_IN_CART"}""");
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfCheckoutService(httpClient);

            var request = new SfCheckoutRequest
            {
                Orders = new[] { "id-invalido" }
            };

            // Act & Assert
            await Assert.ThrowsAsync<SfBadRequestException>(
                () => service.FinalizeOrderAsync(request));
        }
    }
}
