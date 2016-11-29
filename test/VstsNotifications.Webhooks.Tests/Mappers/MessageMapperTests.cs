using System;
using Moq;
using Xunit;
using VstsNotifications.Models;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Mappers;
using VstsNotifications.Models.Payloads.PullRequest;
using VstsNotifications.Services.Models;
using VstsNotifications.Webhooks.Properties;

namespace VstsNotifications.Webhooks.Tests.Mappers
{
    public class MessageMapperTests
    {
        private readonly Mock<IPullRequestInfoMapper> _mockPullRequestInfoMapper;
        private readonly IMessageMapper _messageMapper;

        public MessageMapperTests()
        {
            _mockPullRequestInfoMapper = new Mock<IPullRequestInfoMapper>();
            _messageMapper = new MessageMapper(_mockPullRequestInfoMapper.Object);
        }

        [Fact]
        public void MapMessageWithPayloadAndSettingsNullReturnsEmptyMessage()
        {
            // Arrange
            var pullRequestPayload = (PullRequestPayload) null;
            var settings = (Settings) null;

            // Act
            var message = _messageMapper.MapMessage(pullRequestPayload, settings);

            // Assert
            Assert.NotNull(message);
            Assert.Null(message.SlackWebhookUrl);
            Assert.Empty(message.Contributors);
            Assert.NotNull(message.PullRequestInfo);
        }

        [Fact]
        public void MapMessageWithPayloadAndSettingsInstantiatedReturnsEmptyMessage()
        {
            // Arrange
            var pullRequestPayload = new PullRequestPayload();
            var settings = new Settings();

            _mockPullRequestInfoMapper
                .Setup(x => x.MapPullRequestInfo(pullRequestPayload.Resource))
                .Returns(new PullRequestInfo());

            // Act
            var message = _messageMapper.MapMessage(pullRequestPayload, settings);

            // Assert
            Assert.NotNull(message);
            Assert.Null(message.SlackWebhookUrl);
            Assert.Empty(message.Contributors);
            Assert.NotNull(message.PullRequestInfo);
        }

        [Fact]
        public void MapMessageWithPayloadAndSettingsPopulatedButNoSlackWebhookUrlReturnsEmptyMessageDueToArgumentNullException()
        {
            // Arrange
            var pullRequestPayload = new PullRequestPayload();
            
            var contributorOne = new Contributor { Id = "one@contributor.com", SlackUserId = "one" };
            var contributorTwo = new Contributor { Id = "two@contributor.com", SlackUserId = "two" };
            
            var settings = new Settings
            {
                SlackWebhookUrl = null,
                Contributors = new [] { contributorOne, contributorTwo }
            };

            var reviewerOne = new Collaborator { UniqueName = "rone unique", DisplayName = "rone display" };
            var reviewerTwo = new Collaborator { UniqueName = "rtwo unique", DisplayName = "rtwo display" };

            var pullRequestInfo = new PullRequestInfo 
            { 
                Url = new Uri("https://wwww.myurl.com"),
                Author = new Collaborator { UniqueName = "author unique", DisplayName = "author display" }
            };
            pullRequestInfo.Reviewers.Add(reviewerOne);
            pullRequestInfo.Reviewers.Add(reviewerTwo);

            _mockPullRequestInfoMapper
                .Setup(x => x.MapPullRequestInfo(pullRequestPayload.Resource))
                .Returns(pullRequestInfo);

            // Act
            var message = _messageMapper.MapMessage(pullRequestPayload, settings);

            // Assert
            Assert.NotNull(message);

            _mockPullRequestInfoMapper.Verify(x => x.MapPullRequestInfo(pullRequestPayload.Resource), Times.Never());
        }

        [Fact]
        public void MapMessageWithPayloadAndSettingsPopulatedReturnsMessageWithSettingsAndPullRequestInfo()
        {
            // Arrange
            var pullRequestPayload = new PullRequestPayload();
            
            var contributorOne = new Contributor { Id = "one@contributor.com", SlackUserId = "one" };
            var contributorTwo = new Contributor { Id = "two@contributor.com", SlackUserId = "two" };
            
            var settings = new Settings
            {
                SlackWebhookUrl = "htttp://slack.webhook.com",
                Contributors = new [] { contributorOne, contributorTwo }
            };

            var reviewerOne = new Collaborator { UniqueName = "rone unique", DisplayName = "rone display" };
            var reviewerTwo = new Collaborator { UniqueName = "rtwo unique", DisplayName = "rtwo display" };

            var pullRequestInfo = new PullRequestInfo 
            { 
                Url = new Uri("https://wwww.myurl.com"),
                Author = new Collaborator { UniqueName = "author unique", DisplayName = "author display" }
            };
            pullRequestInfo.Reviewers.Add(reviewerOne);
            pullRequestInfo.Reviewers.Add(reviewerTwo);

            _mockPullRequestInfoMapper
                .Setup(x => x.MapPullRequestInfo(pullRequestPayload.Resource))
                .Returns(pullRequestInfo);

            // Act
            var message = _messageMapper.MapMessage(pullRequestPayload, settings);

            // Assert
            Assert.NotNull(message);
            Assert.Equal(new Uri("htttp://slack.webhook.com"), message.SlackWebhookUrl);
            Assert.Equal(2, settings.Contributors.Length);
            Assert.Equal(contributorOne, settings.Contributors[0]);
            Assert.Equal(contributorTwo, settings.Contributors[1]);
            Assert.Equal(pullRequestInfo, message.PullRequestInfo);
        }
    }
}
