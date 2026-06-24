using System.Net;
using Berdsk.Sdk.SuperFrete.Helpers;
using Berdsk.Sdk.SuperFrete.Services.Webhooks;
using Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos;
using Berdsk.Sdk.SuperFrete.Tests.Unit.Helpers;
using FluentAssertions;

namespace Berdsk.Sdk.SuperFrete.Tests.Unit.Services
{
    public class SfWebhookServiceTests
    {
        private const string WebhookResponseJson =
            """
            {
              "id": "webhook-app-123",
              "name": "Meu Webhook App",
              "url": "https://meusite.com/webhook",
              "secret_token": "abc123def456",
              "events": ["order.posted", "order.delivered"],
              "is_active": true,
              "created_at": "2023-10-01T12:00:00Z"
            }
            """;

        [Fact]
        public async Task CreateWebhookAsync_ValidRequest_ReturnsWebhookResponse()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.Created, WebhookResponseJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfWebhookService(httpClient);

            var request = new SfCreateWebhookRequest
            {
                Name = "Meu Webhook App",
                Url = "https://meusite.com/webhook",
                Events = new[] { SfWebhookEvent.OrderPosted, SfWebhookEvent.OrderDelivered }
            };

            // Act
            var result = await service.CreateWebhookAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be("webhook-app-123");
            result.Name.Should().Be("Meu Webhook App");
            result.SecretToken.Should().Be("abc123def456");
            result.IsActive.Should().BeTrue();
            result.Events.Should().Contain(SfWebhookEvent.OrderPosted);
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/webhook");
            handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        }

        [Fact]
        public async Task ListWebhooksAsync_ReturnsWebhookList()
        {
            // Arrange
            const string listJson =
                """
                [
                  {
                    "id": "webhook-app-123",
                    "name": "Webhook 1",
                    "url": "https://loja1.com/webhook",
                    "events": ["order.posted"],
                    "is_active": true,
                    "created_at": "2023-10-01T12:00:00Z"
                  },
                  {
                    "id": "webhook-app-456",
                    "name": "Webhook 2",
                    "url": "https://loja2.com/webhook",
                    "events": ["order.delivered", "order.cancelled"],
                    "is_active": false,
                    "created_at": "2023-10-05T08:30:00Z"
                  }
                ]
                """;
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, listJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfWebhookService(httpClient);

            // Act
            var result = await service.ListWebhooksAsync();

            // Assert
            result.Should().NotBeNull();
            result!.Should().HaveCount(2);
            result[0].Id.Should().Be("webhook-app-123");
            result[1].IsActive.Should().BeFalse();
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/webhook");
            handler.LastRequest.Method.Should().Be(HttpMethod.Get);
        }

        [Fact]
        public async Task UpdateWebhookAsync_ValidRequest_ReturnsUpdatedWebhook()
        {
            // Arrange
            const string updatedJson =
                """
                {
                  "id": "webhook-app-123",
                  "name": "Webhook Atualizado",
                  "url": "https://novo.com/webhook",
                  "events": ["order.created"],
                  "is_active": false,
                  "updated_at": "2023-10-10T09:00:00Z"
                }
                """;
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, updatedJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfWebhookService(httpClient);

            var request = new SfUpdateWebhookRequest
            {
                Name = "Webhook Atualizado",
                Url = "https://novo.com/webhook",
                Events = new[] { SfWebhookEvent.OrderCreated },
                IsActive = false
            };

            // Act
            var result = await service.UpdateWebhookAsync("webhook-app-123", request);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Webhook Atualizado");
            result.IsActive.Should().BeFalse();
            result.UpdatedAt.Should().NotBeNullOrEmpty();
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/webhook/webhook-app-123");
            handler.LastRequest.Method.Should().Be(HttpMethod.Put);
        }

        [Fact]
        public async Task DeleteWebhookAsync_ValidId_CompletesSuccessfully()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.NoContent, string.Empty);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfWebhookService(httpClient);

            // Act
            var act = async () => await service.DeleteWebhookAsync("webhook-app-123");

            // Assert
            await act.Should().NotThrowAsync();
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/webhook/webhook-app-123");
            handler.LastRequest.Method.Should().Be(HttpMethod.Delete);
        }
    }
}
