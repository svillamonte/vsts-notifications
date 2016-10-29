using VstsNotifications.Services.Models;
using VstsNotifications.Webhooks.Models.PullRequest;

namespace VstsNotifications.Webhooks.Interfaces
{
    public interface IPullRequestInfoMapper
    {
        PullRequestInfo MapPullRequestInfo(PullRequestResource pullRequestResource);
    }
}
