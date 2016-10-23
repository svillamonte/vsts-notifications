using System;
using System.Collections.Generic;

namespace VstsNotifications.Services.Models
{
    public class PullRequestInfo
    {
        public PullRequestInfo ()
        {
            Reviewers = new List<Collaborator>();
        }

        public Uri Url { get; set; }

        public Collaborator Author { get; set; }

        public IList<Collaborator> Reviewers { get; set; }
    }
}
