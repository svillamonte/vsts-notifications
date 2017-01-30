using System.Collections.Generic;

namespace VstsNotifications.Services.Interfaces
{
    public interface ISlackUserMentionBuilder
    {
        string BuildSlackUserMentions(IEnumerable<string> slackHandles);
    }
}
