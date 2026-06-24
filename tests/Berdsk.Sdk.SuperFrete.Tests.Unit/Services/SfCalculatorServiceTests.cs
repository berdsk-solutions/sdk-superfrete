using System.Net;
using Berdsk.Sdk.SuperFrete.Services.Calculator;
using Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos;
using Berdsk.Sdk.SuperFrete.Tests.Unit.Helpers;
using FluentAssertions;

namespace Berdsk.Sdk.SuperFrete.Tests.Unit.Services
{
    public class SfCalculatorServiceTests
    {
        private const string ValidResponseJson =
            """
            [
              {
                "id": 1,
                "name": "PAC",
                "price": 18.61,
                "discount": "5.59",
                "currency": "R$",
                "delivery_time": 5,
                "delivery_range": { "min": 5, "max": 5 },
                "packages": [
                  {
                    "price": 18.61,
                    "discount": "5.59",
                    "format": "box",
                    "dimensions": { "height": "2", "width": "11", "length": "16" },
                    "weight": "0.3",
                    "insurance_value": 0
                  }
                ],
                "additional_services": { "receipt": false, "own_hand": false },
                "company": { "id": 1, "name": "Correios", "picture": "" },
                "has_error": false
              },
              {
                "id": 2,
                "name": "SEDEX",
                "price": 32.10,
                "discount": "8.20",
                "currency": "R$",
                "delivery_time": 2,
                "delivery_range": { "min": 2, "max": 2 },
                "packages": [],
                "additional_services": { "receipt": false, "own_hand": false },
                "company": { "id": 1, "name": "Correios", "picture": "" },
                "has_error": false
              }
            ]
            """;

        [Fact]
        public async Task CalculateShippingAsync_WithPackage_ReturnsShippingResults()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, ValidResponseJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfCalculatorService(httpClient);

            var request = new SfCalculateShippingRequest
            {
                From = new SfCalculationOriginRequest { PostalCode = "01310100" },
                To = new SfCalculationDestinationRequest { PostalCode = "20040020" },
                Package = new SfCalculationPackageRequest
                {
                    Height = 2f,
                    Width = 11f,
                    Length = 16f,
                    Weight = 0.3f
                }
            };

            // Act
            var result = await service.CalculateShippingAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.Should().HaveCount(2);
            result[0].Name.Should().Be("PAC");
            result[0].Id.Should().Be(1);
            result[0].Price.Should().BeApproximately(18.61f, 0.01f);
            result[0].HasError.Should().BeFalse();
            handler.LastRequest!.RequestUri!.ToString().Should().Contain("api/v0/calculator");
            handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        }

        [Fact]
        public async Task CalculateShippingAsync_WithProducts_ReturnsShippingResults()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.OK, ValidResponseJson);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfCalculatorService(httpClient);

            var request = new SfCalculateShippingRequest
            {
                From = new SfCalculationOriginRequest { PostalCode = "01310100" },
                To = new SfCalculationDestinationRequest { PostalCode = "20040020" },
                Products = new[]
                {
                    new SfCalculationProductRequest
                    {
                        Height = 5f,
                        Width = 10f,
                        Length = 15f,
                        Weight = 0.5f,
                        Quantity = 1
                    }
                }
            };

            // Act
            var result = await service.CalculateShippingAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.Should().HaveCount(2);
            result[1].Name.Should().Be("SEDEX");
            result[1].DeliveryTime.Should().Be(2);
        }

        [Fact]
        public async Task CalculateShippingAsync_WhenBadRequest_ThrowsSfBadRequestException()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.BadRequest,
                """{"message":"CEP de origem inválido","code":"INVALID_POSTAL_CODE"}""");
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfCalculatorService(httpClient);

            var request = new SfCalculateShippingRequest
            {
                From = new SfCalculationOriginRequest { PostalCode = "00000000" },
                To = new SfCalculationDestinationRequest { PostalCode = "20040020" },
                Package = new SfCalculationPackageRequest { Height = 2f, Width = 11f, Length = 16f, Weight = 0.3f }
            };

            // Act & Assert
            await Assert.ThrowsAsync<SfBadRequestException>(
                () => service.CalculateShippingAsync(request));
        }

        [Fact]
        public async Task CalculateShippingAsync_WhenUnauthorized_ThrowsSfUnauthorizedException()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(HttpStatusCode.Unauthorized,
                """{"message":"Token inválido ou não fornecido","code":"UNAUTHORIZED"}""");
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://sandbox.superfrete.com/") };
            var service = new SfCalculatorService(httpClient);

            var request = new SfCalculateShippingRequest
            {
                From = new SfCalculationOriginRequest { PostalCode = "01310100" },
                To = new SfCalculationDestinationRequest { PostalCode = "20040020" },
                Package = new SfCalculationPackageRequest { Height = 2f, Width = 11f, Length = 16f, Weight = 0.3f }
            };

            // Act & Assert
            await Assert.ThrowsAsync<SfUnauthorizedException>(
                () => service.CalculateShippingAsync(request));
        }
    }
}
