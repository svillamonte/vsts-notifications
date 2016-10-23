using System;
using Moq;
using Xunit;
using VstsNotifications.Entities;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Mappers;
using VstsNotifications.Webhooks.Models;
using VstsNotifications.Webhooks.Models.PullRequest;
using VstsNotifications.Services.Models;
using System.Collections.Generic;
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
        public void MapMessageWithPayloadAndSettingsNullReturnsNull()
        {
            // Arrange
            var pullRequestPayload = (PullRequestPayload) null;
            var settings = (Settings) null;

            // Act
            var message = _messageMapper.MapMessage(pullRequestPayload, settings);

            // Assert
            Assert.Null(message);
        }

        [Fact]
        public void MapMessageWithOnlyPayloadNullReturnsNull()
        {
            // Arrange
            var pullRequestPayload = (PullRequestPayload) null;
            var settings = new Settings();

            // Act
            var message = _messageMapper.MapMessage(pullRequestPayload, settings);

            // Assert
            Assert.Null(message);
        }

        [Fact]
        public void MapMessageWithOnlySettingsNullReturnsNull()
        {
            // Arrange
            var pullRequestPayload = new PullRequestPayload();
            var settings = (Settings) null;

            // Act
            var message = _messageMapper.MapMessage(pullRequestPayload, settings);

            // Assert
            Assert.Null(message);
        }

        [Fact]
        public void MapMessageWithPayloadAndSettingsInstantiatedNullReturnsEmptyMessage()
        {
            // Arrange
            var pullRequestPayload = new PullRequestPayload();
            var settings = new Settings();

            _mockPullRequestInfoMapper
                .Setup(x => x.MapPullRequestInfo(pullRequestPayload))
                .Returns((PullRequestInfo) null);

            // Act
            var message = _messageMapper.MapMessage(pullRequestPayload, settings);

            // Assert
            Assert.NotNull(message);
            Assert.Null(message.SlackWebhookUrl);
            Assert.Null(message.Contributors);
            Assert.Null(message.PullRequestInfo);
        }

        [Fact]
        public void MapMessageWithPayloadAndSettingsPopulatedReturnsMessageWithSettingsAndPullRequestInfo()
        {
            // Arrange
            var creator = new Models.Contributor { UniqueName = "unique", DisplayName = "display" };

            var pullRequestResource = new PullRequestResource 
            {
                Url = new Uri("https://wwww.myurl.com"),
                CreatedBy = creator
            };

            var pullRequestPayload = new PullRequestPayload { Resource = pullRequestResource };
            
            var contributorOne = new Entities.Contributor { Id = "one@contributor.com", SlackHandle = "one" };
            var contributorTwo = new Entities.Contributor { Id = "two@contributor.com", SlackHandle = "two" };
            
            var settings = new Settings
            {
                SlackWebhookUrl = "htttp://slack.webhook.com",
                Contributors = new [] { contributorOne, contributorTwo }
            };

            var reviewerOne = new Collaborator { UniqueName = "rone unique", DisplayName = "rone display" };
            var reviewerTwo = new Collaborator { UniqueName = "rtwo unique", DisplayName = "rtwo display" };

            var author = new Collaborator { UniqueName = "author unique", DisplayName = "author display" };

            var pullRequestInfo = new PullRequestInfo 
            { 
                Url = pullRequestResource.Url,
                Author = author  
            };
            pullRequestInfo.Reviewers.Add(reviewerOne);
            pullRequestInfo.Reviewers.Add(reviewerTwo);

            _mockPullRequestInfoMapper
                .Setup(x => x.MapPullRequestInfo(pullRequestPayload))
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
