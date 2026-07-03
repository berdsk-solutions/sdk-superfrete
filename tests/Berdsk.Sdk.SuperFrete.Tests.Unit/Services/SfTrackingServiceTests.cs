using System.Net;
using Berdsk.Sdk.SuperFrete.Services.Tracking;
using Berdsk.Sdk.SuperFrete.Tests.Unit.Helpers;
using FluentAssertions;

namespace Berdsk.Sdk.SuperFrete.Tests.Unit.Services
{
    public class SfTrackingServiceTests
    {
        private static string LoadFixture(string fileName)
        {
            return File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "TestData", fileName));
        }

        private static (SfTrackingService Service, MockHttpMessageHandler Handler) CreateService(
            HttpStatusCode statusCode, string json)
        {
            var handler = new MockHttpMessageHandler(statusCode, json);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://rastreamento.superfrete.com/") };
            return (new SfTrackingService(httpClient), handler);
        }

        [Fact]
        public async Task GetTrackingAsync_JadlogShipment_MapsFullResponse()
        {
            // Arrange
            var (service, handler) = CreateService(HttpStatusCode.OK, LoadFixture("tracking-jadlog.json"));

            // Act
            var result = await service.GetTrackingAsync("13190000000001");

            // Assert — raiz
            result.Should().NotBeNull();
            result!.SuperFrete.Should().BeTrue();
            result.IsDelayed.Should().BeFalse();
            result.DelayDays.Should().BeNull();
            result.ApplicationId.Should().Be(100);
            result.ApplicationName.Should().Be("tracking-page");
            result.TicketCreated.Should().BeTrue();
            result.CarrierReplyDeliveryForecasts.Should().NotBeNull().And.BeEmpty();

            // Assert — tracking
            var tracking = result.Tracking!;
            tracking.Label.Should().Be("13190000000001");
            tracking.SenderName.Should().Be("João da Silva");
            tracking.MagentoOrderNumber.Should().Be("2000000001");
            tracking.RecipientName.Should().Be("Maria Oliveira Santos");
            tracking.ShippingModality.Should().Be("JADLOG ECONOMICO");
            // "02/07/2026 21:53:27" é dd/MM/yyyy — deve ser 2 de julho, não 7 de fevereiro
            tracking.CurrentDate.Should().Be(new DateTime(2026, 7, 2, 21, 53, 27, DateTimeKind.Utc));

            // Assert — order_data
            var orderData = tracking.OrderData!;
            orderData.Tag!.Origin!.FirstName.Should().Be("João");
            orderData.Tag.Destination!.City.Should().Be("Guaratinguetá");
            orderData.SuperPoint!.Provider.Should().Be("pegaki");
            orderData.SuperPoint.Carriers.Should().ContainValue("jadlog");
            orderData.SuperPoint.Geolocation!.Latitude.Should().Be(-23.5);
            orderData.SuperPoint.CreatedAt.Should().Be(new DateTime(2026, 2, 2, 21, 40, 40, DateTimeKind.Utc));
            // "13/06/2026 01:30:01" não é parseável como MM/dd — precisa do formato brasileiro
            orderData.SuperPoint.UpdatedAt.Should().Be(new DateTime(2026, 6, 13, 1, 30, 1, DateTimeKind.Utc));
            orderData.SuperPoint.DeletedAt.Should().BeNull();

            // Assert — orders_api_data
            var apiData = tracking.OrdersApiData!;
            apiData.Status.Should().Be("completed");
            apiData.OrderId.Should().Be("01TESTORDERJADLOG00000001");
            apiData.Payment!.AppliedCreditAmount.Should().Be(16.93m);
            apiData.Payment.UseStoreCredit.Should().BeTrue();

            var calculated = apiData.CalculatedService!;
            calculated.Carrier.Should().Be("jadlog");
            calculated.Code.Should().Be("2002");
            calculated.Name.Should().Be("JADLOG Econômico");
            calculated.Total.Should().Be(12.36m);
            calculated.IsContract.Should().BeTrue();
            calculated.DestinationPostcode.Should().Be("12500000");
            calculated.Data!.Width.Should().Be(21m);
            calculated.Data.Height.Should().Be(20.5m);

            var posted = apiData.PostedService!;
            posted.Date.Should().Be(new DateTime(2026, 6, 17, 19, 58, 18, 554, DateTimeKind.Utc));
            posted.WasReturnedToSender.Should().BeFalse();
            // Dimensões postadas vêm como string ("31") e devem ser lidas como número
            posted.Data!.Width.Should().Be(31m);
            posted.Data.DeclaredValueOption.Should().BeNull();

            var carrierData = apiData.CarrierData!;
            carrierData.TrackingCode.Should().Be("13190000000001");
            carrierData.VolumePrint!.BarCode.Should().Be("13190000000001$001012500000");
            carrierData.VolumePrint.Priority.Should().Be(3);
            carrierData.VolumePrint.VolumeSequence.Should().Be(1);

            apiData.MagentoData!.OrderNumber.Should().Be("2000000001");
            apiData.ContentDeclaration.Should().HaveCount(1);
            apiData.ContentDeclaration![0].Quantity.Should().Be(1);
            apiData.ContentDeclaration[0].Value.Should().Be(50.00m);

            // Assert — provider_tracking
            var provider = result.ProviderTracking!;
            provider.Provider.Should().Be("jadlog");
            provider.Success.Should().BeTrue();
            provider.ShipmentId.Should().Be("13190000000001");
            provider.ShipmentStatus.Should().Be("Entregue");
            provider.EstimatedDelivery.Should().Be(new DateTime(2026, 6, 23, 0, 0, 0, DateTimeKind.Utc));
            provider.ShowDelayedBadge.Should().BeFalse();
            provider.Tracking!.Code.Should().Be("18260000000001");
            provider.Tracking.Status.Should().Be("Entregue");
            provider.Tracking.Events.Should().HaveCount(15);
            provider.Tracking.Events![0].Status.Should().Be("Entregue");
            provider.Tracking.Events[0].Date.Should().Be(new DateTime(2026, 6, 19, 15, 57, 23, DateTimeKind.Utc));
            provider.Tracking.Events[0].Unit.Should().Be("CO GUARATINGUETA 01");
            provider.Tracking.Events[0].TrackingOrigin.Should().Be("jadlog");
            provider.Tracking.Events[8].TrackingOrigin.Should().Be("pegaki");

            // Assert — delivery_proof
            var proof = result.DeliveryProof!;
            proof.Carrier.Should().Be("jadlog");
            proof.Source.Should().Be("jadlog_app_tracking_api");
            proof.CapturedAt.Should().Be(new DateTime(2026, 7, 3, 0, 53, 27, 412, DateTimeKind.Utc));
            proof.Links.Should().HaveCount(1);
            proof.Links![0].Rel.Should().Be("pod");
            proof.Links[0].Href.Should().Be("https://example.com/pod/13190000000001.jpeg");

            // Assert — ticket_history
            result.TicketHistory.Should().HaveCount(2);
            result.TicketHistory![0].Id.Should().Be(1500001);
            result.TicketHistory[0].EventType.Should().Be("ticket_history.occurrence_status");
            result.TicketHistory[0].Params!.TrackingStatus.Should().Be("Entregue");
            result.TicketHistory[0].CreatedAt.Should().Be(new DateTime(2026, 7, 3, 0, 53, 27, 127, DateTimeKind.Utc));

            // Assert — requisição
            handler.LastRequest!.Method.Should().Be(HttpMethod.Get);
            handler.LastRequest.RequestUri!.ToString()
                .Should().Be("https://rastreamento.superfrete.com/public/tracking/13190000000001");
        }

        [Fact]
        public async Task GetTrackingAsync_LoggiShipment_MapsFirestoreDateAndStringDimensions()
        {
            // Arrange
            var (service, _) = CreateService(HttpStatusCode.OK, LoadFixture("tracking-loggi.json"));

            // Act
            var result = await service.GetTrackingAsync("ABCDEFGH");

            // Assert — raiz (ticket_created ausente no payload da Loggi)
            result!.TicketCreated.Should().BeNull();

            // Assert — tracking
            var tracking = result.Tracking!;
            tracking.Label.Should().Be("ABCDEFGH");
            tracking.ShippingModality.Should().Be("LOGGI");
            tracking.OrderData!.Tag!.Destination!.Complement.Should().Be("Lote 2");
            tracking.OrderData.SuperPoint!.Favorite.Should().BeTrue();

            // Assert — service_posted com data em formato Firestore Timestamp
            var posted = tracking.OrdersApiData!.PostedService!;
            var expectedDate = DateTimeOffset.FromUnixTimeSeconds(1782937684).UtcDateTime.AddMilliseconds(274);
            posted.Date.Should().Be(expectedDate);
            posted.Code.Should().Be("FREIGHT_TYPE_ECONOMIC");
            posted.Data!.Depth.Should().Be(20m);
            posted.Data.Weight.Should().Be(1m);
            // Campos ausentes no payload da Loggi
            posted.Bonus.Should().BeNull();
            posted.DiscountAmount.Should().BeNull();

            var calculated = tracking.OrdersApiData.CalculatedService!;
            calculated.Carrier.Should().Be("loggi");
            calculated.HomeDelivery.Should().BeTrue();
            calculated.MaxDeliveryTime.Should().Be(8);

            var carrierData = tracking.OrdersApiData.CarrierData!;
            carrierData.Id.Should().Be("7727TESTAAAAAAAAABCDEFGH");
            carrierData.VolumePrint!.BarCode.Should().Be("TESTAAAAABCDEFGH");
            carrierData.VolumePrint.Priority.Should().BeNull();

            // Assert — provider_tracking
            var provider = result.ProviderTracking!;
            provider.Provider.Should().Be("loggi");
            provider.EstimatedDelivery.Should().Be(new DateTime(2026, 7, 3, 0, 0, 0, DateTimeKind.Utc));
            provider.Tracking!.Events.Should().HaveCount(16);
            provider.Tracking.Events![0].Description.Should().Be("O pacote chegou ao destino final.");

            // Assert — delivery_proof com campos extras da Loggi
            var proof = result.DeliveryProof!;
            proof.Links.Should().HaveCount(3);
            proof.Links![1].Rel.Should().Be("delivery_receipt");
            proof.ReceiverName.Should().Be("Ana ");
            proof.ReceiverDocument.Should().BeEmpty();
            proof.LocationDescription.Should().Be("INVALID");
        }

        [Fact]
        public async Task GetTrackingAsync_ViaTrackingClient_UsesTrackingBaseUrl()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, LoadFixture("tracking-jadlog.json"));
            var httpClient = new HttpClient(handler);
            var client = new SuperFreteTrackingClient(httpClient: httpClient);

            // Act
            var result = await client.Tracking.GetTrackingAsync("13190000000001");

            // Assert
            result.Should().NotBeNull();
            httpClient.BaseAddress.Should().Be(new Uri("https://rastreamento.superfrete.com/"));
            handler.LastRequest!.RequestUri!.ToString()
                .Should().StartWith("https://rastreamento.superfrete.com/public/tracking/");
        }

        [Fact]
        public async Task GetTrackingAsync_WhenNotFound_ThrowsSfNotFoundException()
        {
            // Arrange
            var (service, _) = CreateService(HttpStatusCode.NotFound,
                """{"message":"Rastreamento não encontrado","code":"NOT_FOUND"}""");

            // Act & Assert
            await Assert.ThrowsAsync<SfNotFoundException>(
                () => service.GetTrackingAsync("INEXISTENTE"));
        }

        [Fact]
        public async Task GetTrackingAsync_WithEmptyTrackingCode_ThrowsArgumentException()
        {
            // Arrange
            var (service, _) = CreateService(HttpStatusCode.OK, "{}");

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => service.GetTrackingAsync("  "));
        }
    }
}
