using System.Collections.Generic;
using System.Linq;
using VstsNotifications.Models;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services
{
    public class PullRequestMessageService : IPullRequestMessageService
    {
        public IEnumerable<PullRequestMessage> CreatePullRequestMessages(PullRequestInfo pullRequestInfo, IEnumerable<Contributor> contributorsInfo)
        {
            if (!pullRequestInfo.Reviewers.Any()) {
                return new PullRequestMessage [0];
            }

            var pullRequestMessages = new [] 
            {
                new PullRequestMessage {
                    ReviewersSlackUserId = GetReviewersHandle(pullRequestInfo.Reviewers, contributorsInfo),
                    AuthorDisplayName = pullRequestInfo.Author.DisplayName,
                    PullRequestUrl = pullRequestInfo.Url
                }
            };
            
            return pullRequestMessages;
        }

        private IEnumerable<string> GetReviewersHandle(IList<Collaborator> reviewers, IEnumerable<Contributor> contributorsInfo)
        {
            var contributors = contributorsInfo.ToDictionary(key => key.Id, val => val.SlackUserId);

            foreach (var reviewer in reviewers) 
            {
                // Reviewer is not added if can't find the handle.
                if (!contributors.ContainsKey(reviewer.UniqueName))
                {
                    continue;
                }

                yield return contributors[reviewer.UniqueName];
            }
        }
    }
}
