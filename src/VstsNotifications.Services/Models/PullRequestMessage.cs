using System;

namespace VstsNotifications.Services.Models
{
    public class PullRequestMessage
    {
        public string ReviewerSlackUserId { get; set; }

        public string AuthorDisplayName { get; set; }

        public Uri PullRequestUrl { get; set; }
    }
}
