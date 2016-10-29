using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services
{
    public class SlackMessagePayloadService : ISlackMessagePayloadService
    {
        public SlackMessagePayload CreateSlackMessagePayload(PullRequestMessage pullRequestMessage)
        {
            var payload = new SlackMessagePayload { Username = "Visual Studio Team Services" };
            payload.Attachments.Add(new SlackAttachment 
            { 
                Text = 
                    $"Hey @{pullRequestMessage.ReviewerSlackHandle}!, {pullRequestMessage.AuthorDisplayName} " + 
                    $"assigned you a <{pullRequestMessage.PullRequestUrl.OriginalString}|pull request>." 
            });

            return payload;
        }
    }
}
