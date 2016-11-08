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
            var contributors = contributorsInfo.ToDictionary(key => key.Id, val => val.SlackUserId);

            foreach (var reviewer in pullRequestInfo.Reviewers)
            {
                // No message is created if can't find reviewer's handle.
                if (!contributors.ContainsKey(reviewer.UniqueName))
                {
                    continue;
                }

                yield return new PullRequestMessage
                {
                    ReviewerSlackUserId = contributors[reviewer.UniqueName],
                    AuthorDisplayName = pullRequestInfo.Author.DisplayName,
                    PullRequestUrl = pullRequestInfo.Url
                };
            }
        }
    }
}
