using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Moq;
using Moq.Protected;
using StrykerDemo.ApiClient;
using Xunit;

namespace StrykerDemo.Tests.ApiClient
{
    public class WebApiClientTests
    {
        [Fact]
        public async Task GetData_WithNormalFlow_ReturnsData()
        {
            // Arrange
            Faker faker = new Faker();

            string expectedResults = faker.Random.String2(100);

            Mock<HttpMessageHandler> httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedResults, Encoding.UTF8, "application/json")
                });

            HttpClient httpClient = new HttpClient(httpMessageHandlerMock.Object);
            httpClient.BaseAddress = new Uri(faker.Internet.Url());

            WebApiClient webApiClient = new WebApiClient(httpClient);

            // Act
            string actualResults = await webApiClient.GetData();

            // Assert
            Assert.Equal(expectedResults, actualResults);
        }

        [Fact]
        public async Task GetData_WithBadRequest_ThrowsHttpRequestException()
        {
            // Arrange
            Faker faker = new Faker();

            Mock<HttpMessageHandler> httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            HttpClient httpClient = new HttpClient(httpMessageHandlerMock.Object);
            httpClient.BaseAddress = new Uri(faker.Internet.Url());

            WebApiClient webApiClient = new WebApiClient(httpClient);

            // Act and Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => webApiClient.GetData());
        }
    }
}
