using System;
using Moq;
using Xunit;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;
using VstsNotifications.Models;

namespace VstsNotifications.Services.Tests
{
    public class MessageServiceTests
    {
        private readonly UserGroup _defaultUserGroup;
        private readonly Mock<IPullRequestMessageService> _mockPullRequestMessageService;
        private readonly Mock<ISlackMessagePayloadService> _mockSlackMessagePayloadService;
        private readonly Mock<ISlackClient> _mockSlackClient;

        private readonly IMessageService _messageService;
        
        public MessageServiceTests()
        {
            _defaultUserGroup = new UserGroup { SlackHandle = "the-handle", SlackUserGroupId = "usergroupid" };

            _mockPullRequestMessageService = new Mock<IPullRequestMessageService>();
            _mockSlackMessagePayloadService = new Mock<ISlackMessagePayloadService>();
            _mockSlackClient = new Mock<ISlackClient>();

            _messageService = new MessageService(_mockPullRequestMessageService.Object, _mockSlackMessagePayloadService.Object, _mockSlackClient.Object);
        }

        [Fact]
        public void NotifyReviewersWithMessageNullReturnsFalse()
        {
            // Arrange
            var message = (Message) null;

            // Act
            var result = _messageService.NotifyReviewers(message);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void NotifyReviewersWithMessageEmptyReturnsTrueButDoesNothing()
        {
            // Arrange
            var message = new Message();

            _mockPullRequestMessageService
                .Setup(x => x.CreatePullRequestMessages(message.PullRequestInfo, message.Contributors))
                .Returns(new PullRequestMessage[0]);

            // Act
            var result = _messageService.NotifyReviewers(message);

            // Assert
            Assert.True(result);
            _mockSlackMessagePayloadService.Verify(x => x.CreateSlackMessagePayload(It.IsAny<PullRequestMessage>(), _defaultUserGroup), Times.Never());
            _mockSlackClient.Verify(x => x.PostMessageAsync(It.IsAny<SlackMessagePayload>(), It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void NotifyReviewersWithMessagePopulatedButNoSlackWebhookUrlReturnsFalseDueToNullReferenceException()
        {
            // Arrange
            var message = new Message
            {
                SlackWebhookUrl = null,
                PullRequestInfo = new PullRequestInfo(),
                DefaultUserGroup = _defaultUserGroup
            };

            var pullRequestMessageOne = new PullRequestMessage { ReviewersSlackUserId = new [] { "shone" } }; 
            var pullRequestMessageTwo = new PullRequestMessage { ReviewersSlackUserId = new [] { "shtwo" } }; 

            var payloadOne = new SlackMessagePayload { Username = "Payload one" };
            var payloadTwo = new SlackMessagePayload { Username = "Payload two" };

            _mockPullRequestMessageService
                .Setup(x => x.CreatePullRequestMessages(message.PullRequestInfo, message.Contributors))
                .Returns(new [] { pullRequestMessageOne, pullRequestMessageTwo });

            _mockSlackMessagePayloadService
                .Setup(x => x.CreateSlackMessagePayload(pullRequestMessageOne, _defaultUserGroup))
                .Returns(payloadOne);
            _mockSlackMessagePayloadService
                .Setup(x => x.CreateSlackMessagePayload(pullRequestMessageTwo, _defaultUserGroup))
                .Returns(payloadTwo);

            // Act
            var result = _messageService.NotifyReviewers(message);

            // Assert
            Assert.False(result);

            _mockSlackMessagePayloadService.Verify(x => x.CreateSlackMessagePayload(pullRequestMessageOne, _defaultUserGroup), Times.Once());
            _mockSlackMessagePayloadService.Verify(x => x.CreateSlackMessagePayload(pullRequestMessageTwo, _defaultUserGroup), Times.Never());
            
            _mockSlackClient.Verify(x => x.PostMessageAsync(payloadOne, It.IsAny<string>()), Times.Never());
            _mockSlackClient.Verify(x => x.PostMessageAsync(payloadTwo, It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void NotifyReviewersWithMessagePopulatedButNoPullRequestMessagesReturnsTrueButDoesNotInvokeMethods()
        {
            // Arrange
            var message = new Message
            {
                SlackWebhookUrl = new Uri("https://my.webhook.com"),
                PullRequestInfo = new PullRequestInfo(),
                DefaultUserGroup = _defaultUserGroup
            };

            _mockPullRequestMessageService
                .Setup(x => x.CreatePullRequestMessages(message.PullRequestInfo, message.Contributors))
                .Returns(new PullRequestMessage[0]);

            // Act
            var result = _messageService.NotifyReviewers(message);

            // Assert
            Assert.True(result);

            _mockSlackMessagePayloadService.Verify(x => x.CreateSlackMessagePayload(It.IsAny<PullRequestMessage>(), _defaultUserGroup), Times.Never());            
            _mockSlackClient.Verify(x => x.PostMessageAsync(It.IsAny<SlackMessagePayload>(), It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void NotifyReviewersWithMessagePopulatedAndAllExpectedValuesReturnsTrueAndInvokesMethodsAsExpected()
        {
            // Arrange
            var message = new Message
            {
                SlackWebhookUrl = new Uri("https://my.webhook.com"),
                PullRequestInfo = new PullRequestInfo(),
                DefaultUserGroup = _defaultUserGroup
            };

            var pullRequestMessageOne = new PullRequestMessage { ReviewersSlackUserId = new [] { "shone" } }; 
            var pullRequestMessageTwo = new PullRequestMessage { ReviewersSlackUserId = new [] { "shtwo" } }; 

            var payloadOne = new SlackMessagePayload { Username = "Payload one" };
            var payloadTwo = new SlackMessagePayload { Username = "Payload two" };

            _mockPullRequestMessageService
                .Setup(x => x.CreatePullRequestMessages(message.PullRequestInfo, message.Contributors))
                .Returns(new [] { pullRequestMessageOne, pullRequestMessageTwo });

            _mockSlackMessagePayloadService
                .Setup(x => x.CreateSlackMessagePayload(pullRequestMessageOne, _defaultUserGroup))
                .Returns(payloadOne);
            _mockSlackMessagePayloadService
                .Setup(x => x.CreateSlackMessagePayload(pullRequestMessageTwo, _defaultUserGroup))
                .Returns(payloadTwo);

            // Act
            var result = _messageService.NotifyReviewers(message);

            // Assert
            Assert.True(result);

            _mockSlackMessagePayloadService.Verify(x => x.CreateSlackMessagePayload(pullRequestMessageOne, _defaultUserGroup), Times.Once());
            _mockSlackMessagePayloadService.Verify(x => x.CreateSlackMessagePayload(pullRequestMessageTwo, _defaultUserGroup), Times.Once());
            
            _mockSlackClient.Verify(x => x.PostMessageAsync(payloadOne, message.SlackWebhookUrl.OriginalString), Times.Once());
            _mockSlackClient.Verify(x => x.PostMessageAsync(payloadTwo, message.SlackWebhookUrl.OriginalString), Times.Once());
        }
    }
}
