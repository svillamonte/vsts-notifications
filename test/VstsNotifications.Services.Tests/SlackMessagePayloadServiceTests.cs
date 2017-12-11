using System;
using Moq;
using Xunit;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;
using System.Collections.Generic;
using VstsNotifications.Models;

namespace VstsNotifications.Services.Tests
{
    public class SlackMessagePayloadServiceTests
    {
        private readonly UserGroup _defaultUserGroup;
        private readonly Mock<ISlackUserMentionBuilder> _mockSlackUserMentionBuilder;
        private readonly ISlackMessagePayloadService _slackMessagePayloadService;

        public SlackMessagePayloadServiceTests ()
        {
            _defaultUserGroup = new UserGroup { SlackHandle = "the-handle", SlackUserGroupId = "usergroupid" };
            _mockSlackUserMentionBuilder = new Mock<ISlackUserMentionBuilder>();

            _slackMessagePayloadService = new SlackMessagePayloadService(_mockSlackUserMentionBuilder.Object);          
        }

        [Fact]
        public void CreateSlackMessagePayloadWithEmptyPullRequestMessageReturnsNotEvaluatedMessage()
        {
            // Arrange
            var pullRequestMessage = new PullRequestMessage();

            // Act

            // Assert
            Assert.Throws<NullReferenceException>(() => _slackMessagePayloadService.CreateSlackMessagePayload(pullRequestMessage, _defaultUserGroup));
        }

        [Fact]
        public void CreateSlackMessagePayloadWithPullRequestMessageWithOneReviewerReturnsPayloadWithAttachment()
        {
            // Arrange
            var pullRequestMessage = new PullRequestMessage
            {
                ReviewersSlackUserId = new [] { "suserid1", "suserid2" },
                AuthorDisplayName = "Author name",
                PullRequestUrl = new Uri("https://my.pullrequest.com")
            };

            _mockSlackUserMentionBuilder
                .Setup(x => x.BuildSlackUserMentions(pullRequestMessage.ReviewersSlackUserId, _defaultUserGroup))
                .Returns("<@suserid1>, <@suserid2>");

            // Act
            var payload = _slackMessagePayloadService.CreateSlackMessagePayload(pullRequestMessage, _defaultUserGroup);

            // Assert
            Assert.NotNull(payload);
            Assert.Equal("Visual Studio Team Services", payload.Username);
            Assert.Equal("Hey <@suserid1>, <@suserid2>!, Author name assigned you a <https://my.pullrequest.com|pull request>.", payload.Attachments[0].Text);
        }
    }
}
