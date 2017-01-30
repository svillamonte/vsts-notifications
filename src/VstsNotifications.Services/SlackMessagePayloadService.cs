using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services
{
    public class SlackMessagePayloadService : ISlackMessagePayloadService
    {
        private readonly ISlackUserMentionBuilder _slackUserMentionBuilder;
        
        public SlackMessagePayloadService(ISlackUserMentionBuilder slackUserMentionBuilder)
        {
            _slackUserMentionBuilder = slackUserMentionBuilder;
        }

        public SlackMessagePayload CreateSlackMessagePayload(PullRequestMessage pullRequestMessage)
        {
            var payload = new SlackMessagePayload { Username = "Visual Studio Team Services" };
            payload.Attachments.Add(new SlackAttachment 
            { 
                Text = 
                    $"Hey {_slackUserMentionBuilder.BuildSlackUserMentions(pullRequestMessage.ReviewersSlackUserId)}!, " +
                    $"{pullRequestMessage.AuthorDisplayName} " + 
                    $"assigned you a <{pullRequestMessage.PullRequestUrl.OriginalString}|pull request>." 
            });

            return payload;
        }
    }
}
