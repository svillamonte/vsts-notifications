using System.Collections.Generic;
using System.Linq;
using VstsNotifications.Models;
using VstsNotifications.Services.Interfaces;

namespace VstsNotifications.Services.Builders
{
    public class SlackUserMentionBuilder : ISlackUserMentionBuilder
    {
        public string BuildSlackUserMentions(IEnumerable<string> slackHandles, UserGroup defaultUserGroup) 
        {
            if (!slackHandles.Any())
            {
                return BuildTreatedUserGroup(defaultUserGroup);
            }

            var treatedHandles = slackHandles.Select(x => $"<@{x}>");

            if (slackHandles.Count() == 1)
            {
                return treatedHandles.First();
            }            

            var firstItems = treatedHandles.Take(slackHandles.Count() - 1);
            var lastItem = treatedHandles.Last();

            var reviewers = string.Join(", ", firstItems);
            reviewers += $" and {lastItem}";

            return reviewers;
        }

        private string BuildTreatedUserGroup(UserGroup userGroup)
        {
            return $"<!subteam^{userGroup.SlackUserGroupId}|{userGroup.SlackHandle}>";
        }
    }
}
