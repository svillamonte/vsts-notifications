using System;
using VstsNotifications.Models;
using VstsNotifications.Services.Builders;
using VstsNotifications.Services.Interfaces;
using Xunit;

namespace VstsNotifications.Services.Tests.Builders
{
    public class SlackUserMentionBuilderTests
    {
        private readonly UserGroup _defaultUserGroup;
        private readonly ISlackUserMentionBuilder _slackUserMentionBuilder;

        public SlackUserMentionBuilderTests()
        {
            _defaultUserGroup = new UserGroup { SlackHandle = "the-handle", SlackUserGroupId = "usergroupid" };
            _slackUserMentionBuilder = new SlackUserMentionBuilder();
        }

        [Fact]
        public void BuildSlackUserMentionsForNoHandleReturnsDefaultGroupHandle() 
        {
            // Arrange
            var handles = new string [0];

            // Act
            var result = _slackUserMentionBuilder.BuildSlackUserMentions(handles, _defaultUserGroup);

            // Assert
            Assert.Equal("<!subteam^usergroupid|the-handle>", result);
        }

        [Fact]
        public void BuildSlackUserMentionsForOneHandleReturnsNoJoinedItems() 
        {
            // Arrange
            var handles = new [] { "hone" };

            // Act
            var slackMentions = _slackUserMentionBuilder.BuildSlackUserMentions(handles, _defaultUserGroup);

            // Assert
            Assert.Equal("<@hone>", slackMentions);
        }

        [Fact]
        public void BuildSlackUserMentionsForTwoHandlesReturnsItemsJoinedByAnd() 
        {
            // Arrange
            var handles = new [] { "hone", "htwo" };

            // Act
            var slackMentions = _slackUserMentionBuilder.BuildSlackUserMentions(handles, _defaultUserGroup);

            // Assert
            Assert.Equal("<@hone> and <@htwo>", slackMentions);
        }

        [Fact]
        public void BuildSlackUserMentionsForMoreThanTwoHandlesReturnsAllItemsJoinedByCommaButTheLastOne() 
        {
            // Arrange
            var handles = new [] { "hone", "htwo", "hthree", "hfour" };

            // Act
            var slackMentions = _slackUserMentionBuilder.BuildSlackUserMentions(handles, _defaultUserGroup);

            // Assert
            Assert.Equal("<@hone>, <@htwo>, <@hthree> and <@hfour>", slackMentions);
        }
    }
}
