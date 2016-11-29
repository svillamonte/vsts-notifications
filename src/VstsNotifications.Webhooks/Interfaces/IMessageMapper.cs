using VstsNotifications.Models.Payloads.PullRequest;
using VstsNotifications.Services.Models;
using VstsNotifications.Webhooks.Properties;

namespace VstsNotifications.Webhooks.Interfaces
{
    public interface IMessageMapper
    {
        Message MapMessage(PullRequestPayload pullRequestPayload, Settings settings);
    }
}
