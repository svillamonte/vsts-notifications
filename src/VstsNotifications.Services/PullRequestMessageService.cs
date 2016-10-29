using System.Collections.Generic;
using System.Linq;
using VstsNotifications.Entities;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services
{
    public class PullRequestMessageService : IPullRequestMessageService
    {
        public IEnumerable<PullRequestMessage> CreatePullRequestMessages(PullRequestInfo pullRequestInfo, IEnumerable<Contributor> contributorsInfo)
        {
            var contributors = contributorsInfo.ToDictionary(key => key.Id, val => val.SlackHandle);

            foreach (var reviewer in pullRequestInfo.Reviewers)
            {
                yield return new PullRequestMessage
                {
                    ReviewerSlackHandle = contributors[reviewer.UniqueName],
                    AuthorDisplayName = pullRequestInfo.Author.DisplayName,
                    PullRequestUrl = pullRequestInfo.Url
                };
            }
        }
    }
}
