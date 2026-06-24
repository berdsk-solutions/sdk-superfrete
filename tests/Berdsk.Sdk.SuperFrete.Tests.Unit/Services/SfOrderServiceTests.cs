using System.Collections.Generic;
using System.Net;
using Berdsk.Sdk.SuperFrete.Services.Orders;
using Berdsk.Sdk.SuperFrete.Services.Orders.Dtos;
using Berdsk.Sdk.SuperFrete.Tests.Unit.Helpers;
using FluentAssertions;

namespace Berdsk.Sdk.SuperFrete.Tests.Unit.Services
{
    public class SfOrderServiceTests
    {
        private const string OrderInfoJson =
            """
            {
              "id": "r8p3dhqjn4I0tBpnvkpU",
              "protocol": "ORD-2024-001",
              "status": "released",
              "tracking": "DG048745602BR",
              "price": 33.33,
              "created_at": "2024-03-29T23:49:26+00:00",
              "updated_at": "2024-03-29T23:51:47+00:00"
            }
            """;

        [Fact]
        public async Task GetOrderInfoAsync_ValidId_ReturnsOrderInfo()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, OrderInfoJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfOrderService(httpClient);

            // Act
            var result = await service.GetOrderInfoAsync("r8p3dhqjn4I0tBpnvkpU");

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be("r8p3dhqjn4I0tBpnvkpU");
            result.Status.Should().Be("released");
            result.Tracking.Should().Be("DG048745602BR");
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/order/info/r8p3dhqjn4I0tBpnvkpU");
            handler.LastRequest.Method.Should().Be(HttpMethod.Get);
        }

        [Fact]
        public async Task GetOrderInfoAsync_WhenNotFound_ThrowsSfNotFoundException()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.NotFound,
                """{"message":"Pedido não encontrado","code":"ORDER_NOT_FOUND"}""");
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfOrderService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<SfNotFoundException>(
                () => service.GetOrderInfoAsync("id-inexistente"));
        }

        [Fact]
        public async Task CancelOrderAsync_ValidRequest_ReturnsCancelResult()
        {
            // Arrange
            // A API retorna um Dictionary<string, SfCancelOrderResultResponse> onde
            // a chave é o ID da etiqueta e o valor contém a propriedade "canceled"
            const string cancelJson =
                """
                {
                  "r8p3dhqjn4I0tBpnvkpU": { "canceled": true }
                }
                """;
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, cancelJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfOrderService(httpClient);

            var request = new SfCancelOrderRequest
            {
                Order = new SfCancelOrderItemRequest
                {
                    Id = "r8p3dhqjn4I0tBpnvkpU",
                    Description = "Pedido cancelado pelo cliente"
                }
            };

            // Act
            var result = await service.CancelOrderAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.Should().ContainKey("r8p3dhqjn4I0tBpnvkpU");
            result["r8p3dhqjn4I0tBpnvkpU"].Canceled.Should().BeTrue();
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/order/cancel");
            handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        }

        [Fact]
        public async Task GetPrintLinkAsync_ValidRequest_ReturnsPrintLink()
        {
            // Arrange
            const string printJson = """{"url":"https://sandbox.superfrete.com/print/etiqueta.pdf"}""";
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, printJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfOrderService(httpClient);

            var request = new SfPrintLinkRequest
            {
                Orders = new[] { "r8p3dhqjn4I0tBpnvkpU" }
            };

            // Act
            var result = await service.GetPrintLinkAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.Url.Should().Contain("etiqueta.pdf");
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/tag/print");
            handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        }

        [Fact]
        public async Task ListOrdersAsync_WithNoFilters_ReturnsOrders()
        {
            // Arrange
            const string listJson =
                """
                {
                  "data": [
                    {
                      "id": "r8p3dhqjn4I0tBpnvkpU",
                      "status": "released",
                      "tracking": "DG048745602BR",
                      "price": 33.33,
                      "created_at": "2024-03-29T23:49:26+00:00"
                    },
                    {
                      "id": "abc123def456ghij7890",
                      "status": "pending",
                      "tracking": null,
                      "price": 18.61,
                      "created_at": "2024-03-28T10:00:00+00:00"
                    }
                  ],
                  "total": 2,
                  "per_page": 20,
                  "current_page": 1,
                  "last_page": 1
                }
                """;
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, listJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfOrderService(httpClient);

            // Act
            var result = await service.ListOrdersAsync();

            // Assert
            result.Should().NotBeNull();
            result!.Data.Should().NotBeNull();
            result.Data!.Should().HaveCount(2);
            result.Data[0].Id.Should().Be("r8p3dhqjn4I0tBpnvkpU");
            result.Data[1].Status.Should().Be("pending");
            result.Total.Should().Be(2);
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/me/orders");
            handler.LastRequest.Method.Should().Be(HttpMethod.Get);
        }
    }
}
