using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Tests
{
    public class SlackClientTests
    {
        private readonly Mock<IHttpClient> _mockHttpClient;
        private readonly ISlackClient _slackClient;

        public SlackClientTests ()
        {
            _mockHttpClient = new Mock<IHttpClient>();
            _slackClient = new SlackClient(_mockHttpClient.Object);
        }

        [Fact]
        public async Task PostMessageAsyncWithAcceptedResponseReturnsTrue()
        {
            // Arrange
            const string webhookUrl = "https://some.good.url/";

            var httpResponseMessage = new HttpResponseMessage
            {
                Content = new StringContent("ok", Encoding.UTF8, "text/html"),
                StatusCode = HttpStatusCode.Accepted
            };
            
            _mockHttpClient
                .Setup(x => x.PostAsync(webhookUrl, It.IsAny<HttpContent>()))
                .ReturnsAsync(httpResponseMessage);

            // Act
            var response = await _slackClient.PostMessageAsync(new SlackMessagePayload(), webhookUrl);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async Task PostMessageAsyncWithBadRequestResponseReturnsFalse()
        {
            // Arrange
            const string webhookUrl = "https://some.good.url/";

            var httpResponseMessage = new HttpResponseMessage
            {
                Content = new StringContent("invalid", Encoding.UTF8, "text/html"),
                StatusCode = HttpStatusCode.BadRequest
            };
            
            _mockHttpClient
                .Setup(x => x.PostAsync(webhookUrl, It.IsAny<HttpContent>()))
                .ReturnsAsync(httpResponseMessage);
                
            // Act
            var response = await _slackClient.PostMessageAsync(new SlackMessagePayload(), webhookUrl);

            // Assert
            Assert.False(response);
        }

        [Fact]
        public async Task PostMessageAsyncThrowsNullReferenceExceptionReturnsFalse()
        {
            // Arrange
            const string webhookUrl = "https://some.good.url/";
            
            _mockHttpClient
                .Setup(x => x.PostAsync(webhookUrl, It.IsAny<HttpContent>()))
                .Throws(new NullReferenceException());
                
            // Act
            var response = await _slackClient.PostMessageAsync(new SlackMessagePayload(), webhookUrl);

            // Assert
            Assert.False(response);
        }
    }
}
