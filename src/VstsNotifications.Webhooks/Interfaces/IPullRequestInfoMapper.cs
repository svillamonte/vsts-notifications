using VstsNotifications.Models.Payloads.PullRequest;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Webhooks.Interfaces
{
    public interface IPullRequestInfoMapper
    {
        PullRequestInfo MapPullRequestInfo(PullRequestResource pullRequestResource);
    }
}
