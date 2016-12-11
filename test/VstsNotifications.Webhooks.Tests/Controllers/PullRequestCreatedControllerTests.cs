using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using VstsNotifications.Models.Payloads.PullRequest;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;
using VstsNotifications.Webhooks.Controllers;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Properties;

namespace VstsNotifications.Webhooks.Tests.Mappers
{
    public class PullRequestCreatedControllerTests
    {
        private readonly Settings _settings;
        private readonly Mock<IOptions<Settings>> _mockSettings;
        private readonly Mock<IMessageMapper> _mockMessageMapper;
        private readonly Mock<IMessageService> _mockMessageService;
        private readonly PullRequestCreatedController _pullRequestCreatedController;

        public PullRequestCreatedControllerTests()
        {
            _settings = new Settings();
            _mockSettings = new Mock<IOptions<Settings>>();
            _mockSettings.Setup(x => x.Value).Returns(_settings);

            _mockMessageMapper = new Mock<IMessageMapper>();
            _mockMessageService = new Mock<IMessageService>();

            _pullRequestCreatedController = new PullRequestCreatedController(_mockSettings.Object, _mockMessageMapper.Object, _mockMessageService.Object);
        }

        [Fact]
        public void GetReturnsReadyMessage()
        {
            // Arrange

            // Act
            var actionResult = _pullRequestCreatedController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal("Ready!", okResult.Value);
        }

        [Fact]
        public void PostWithEverythingCheckingOutReturnsMessagePostedMessage()
        {
            // Arrange
            var payload = new PullRequestPayload();
            var message = new Message();
            
            _mockMessageMapper
                .Setup(x => x.MapMessage(payload, _settings))
                .Returns(message);
            _mockMessageService
                .Setup(x => x.NotifyReviewers(message))
                .Returns(true);

            // Act
            var actionResult = _pullRequestCreatedController.Post(payload);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal("Message posted!", okResult.Value);
        }

        [Fact]
        public void PostWithMessageServiceReturningFalseReturnsInternalServerError()
        {
            // Arrange
            var payload = new PullRequestPayload();
            var message = new Message();
            
            _mockMessageMapper
                .Setup(x => x.MapMessage(payload, _settings))
                .Returns(message);
            _mockMessageService
                .Setup(x => x.NotifyReviewers(message))
                .Returns(false);

            // Act
            var actionResult = _pullRequestCreatedController.Post(payload);

            // Assert
            var internalServerResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal((int) HttpStatusCode.InternalServerError, internalServerResult.StatusCode);
        }

        [Fact]
        public void PostWithMessageMapperThrowingAnExceptionReturnsInternalServerError()
        {
            // Arrange
            var payload = new PullRequestPayload();
            var message = new Message();
            
            _mockMessageMapper
                .Setup(x => x.MapMessage(payload, _settings))
                .Throws(new Exception());
            _mockMessageService
                .Setup(x => x.NotifyReviewers(message))
                .Returns(true);

            // Act
            var actionResult = _pullRequestCreatedController.Post(payload);

            // Assert
            var internalServerResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal((int) HttpStatusCode.InternalServerError, internalServerResult.StatusCode);
        }
    }
}
