using System;
using System.Collections.Generic;

namespace VstsNotifications.Services.Models
{
    public class PullRequestMessage
    {
        public IEnumerable<string> ReviewersSlackUserId { get; set; }

        public string AuthorDisplayName { get; set; }

        public Uri PullRequestUrl { get; set; }
    }
}
