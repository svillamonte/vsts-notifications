using System.Collections.Generic;
using VstsNotifications.Models;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Interfaces
{
    public interface IPullRequestMessageService
    {
        IEnumerable<PullRequestMessage> CreatePullRequestMessages(PullRequestInfo pullRequestInfo, IEnumerable<Contributor> contributorsInfo);
    }
}
