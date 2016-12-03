using VstsNotifications.Models.Payloads.PullRequest;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Interfaces
{
    public interface IPullRequestInfoMapper
    {
        PullRequestInfo MapPullRequestInfo(PullRequestResource pullRequestResource);
    }
}
