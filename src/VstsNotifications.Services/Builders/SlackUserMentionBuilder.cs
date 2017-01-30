using System.Collections.Generic;
using System.Linq;
using VstsNotifications.Services.Interfaces;

namespace VstsNotifications.Services.Builders
{
    public class SlackUserMentionBuilder : ISlackUserMentionBuilder
    {
        public string BuildSlackUserMentions(IEnumerable<string> slackHandles) 
        {
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
    }
}
