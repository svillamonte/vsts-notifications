using VstsNotifications.Services.Models;
using VstsNotifications.Models.Payloads.PullRequest;

namespace VstsNotifications.Webhooks.Interfaces
{
    public interface IPullRequestInfoMapper
    {
        PullRequestInfo MapPullRequestInfo(PullRequestResource pullRequestResource);
    }
}
