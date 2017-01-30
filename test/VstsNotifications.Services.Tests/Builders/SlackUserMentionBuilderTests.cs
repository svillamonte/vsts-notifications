using System;
using VstsNotifications.Services.Builders;
using VstsNotifications.Services.Interfaces;
using Xunit;

namespace VstsNotifications.Services.Tests.Builders
{
    public class SlackUserMentionBuilderTests
    {
        private readonly ISlackUserMentionBuilder _slackUserMentionBuilder;

        public SlackUserMentionBuilderTests()
        {
            _slackUserMentionBuilder = new SlackUserMentionBuilder();
        }

        [Fact]
        public void BuildSlackUserMentionsForNoHandleThrowsInvalidOperationException() 
        {
            // Arrange
            var handles = new string [0];

            // Act

            // Assert
            Assert.Throws<InvalidOperationException>(() => _slackUserMentionBuilder.BuildSlackUserMentions(handles));
        }

        [Fact]
        public void BuildSlackUserMentionsForOneHandleReturnsNoJoinedItems() 
        {
            // Arrange
            var handles = new [] { "hone" };

            // Act
            var slackMentions = _slackUserMentionBuilder.BuildSlackUserMentions(handles);

            // Assert
            Assert.Equal("<@hone>", slackMentions);
        }

        [Fact]
        public void BuildSlackUserMentionsForTwoHandlesReturnsItemsJoinedByAnd() 
        {
            // Arrange
            var handles = new [] { "hone", "htwo" };

            // Act
            var slackMentions = _slackUserMentionBuilder.BuildSlackUserMentions(handles);

            // Assert
            Assert.Equal("<@hone> and <@htwo>", slackMentions);
        }

        [Fact]
        public void BuildSlackUserMentionsForMoreThanTwoHandlesReturnsAllItemsJoinedByCommaButTheLastOne() 
        {
            // Arrange
            var handles = new [] { "hone", "htwo", "hthree", "hfour" };

            // Act
            var slackMentions = _slackUserMentionBuilder.BuildSlackUserMentions(handles);

            // Assert
            Assert.Equal("<@hone>, <@htwo>, <@hthree> and <@hfour>", slackMentions);
        }
    }
}
