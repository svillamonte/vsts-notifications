using System.Collections.Generic;
using VstsNotifications.Models;

namespace VstsNotifications.Services.Interfaces
{
    public interface ISlackUserMentionBuilder
    {
        string BuildSlackUserMentions(IEnumerable<string> slackHandles, UserGroup defaultUserGroup);
    }
}
