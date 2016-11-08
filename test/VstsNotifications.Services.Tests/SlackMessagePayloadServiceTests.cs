using Xunit;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;
using System;

namespace VstsNotifications.Services.Tests
{
    public class SlackMessagePayloadServiceTests
    {
        private readonly ISlackMessagePayloadService _slackMessagePayloadService;

        public SlackMessagePayloadServiceTests ()
        {
            _slackMessagePayloadService = new SlackMessagePayloadService();          
        }

        [Fact]
        public void CreateSlackMessagePayloadWithEmptyPullRequestMessageReturnsNotEvaluatedMessage()
        {
            // Arrange
            var pullRequestMessage = new PullRequestMessage();

            // Act

            // Assert
            Assert.Throws<NullReferenceException>(() => _slackMessagePayloadService.CreateSlackMessagePayload(pullRequestMessage));
        }

        [Fact]
        public void CreateSlackMessagePayloadWithPullRequestMessageReturnsPayloadWithAttachment()
        {
            // Arrange
            var pullRequestMessage = new PullRequestMessage
            {
                ReviewerSlackUserId = "suserid",
                AuthorDisplayName = "Author name",
                PullRequestUrl = new Uri("https://my.pullrequest.com")
            };

            // Act
            var payload = _slackMessagePayloadService.CreateSlackMessagePayload(pullRequestMessage);

            // Assert
            Assert.NotNull(payload);
            Assert.Equal("Visual Studio Team Services", payload.Username);
            Assert.Equal("Hey <@suserid>!, Author name assigned you a <https://my.pullrequest.com|pull request>.", payload.Attachments[0].Text);
        }
    }
}
