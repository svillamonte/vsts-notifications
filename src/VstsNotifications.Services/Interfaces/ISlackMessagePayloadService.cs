using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Interfaces
{
    public interface ISlackMessagePayloadService
    {
        SlackMessagePayload CreateSlackMessagePayload(PullRequestMessage pullRequestMessage);
    }
}
