using System;

namespace VstsNotifications.Services.Models
{
    public class PullRequestMessage
    {
        public string ReviewerSlackHandle { get; set; }

        public string AuthorDisplayName { get; set; }

        public Uri PullRequestUrl { get; set; }
    }
}
