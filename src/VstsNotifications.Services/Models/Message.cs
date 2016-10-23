using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VstsNotifications.Entities;

namespace VstsNotifications.Services.Models
{
    public class Message
    {
        public Message()
        {
            Contributors = new Collection<Contributor>();
        }

        public Uri SlackWebhookUrl { get; set; }

        public ICollection<Contributor> Contributors { get; set; }

        public PullRequestInfo PullRequestInfo { get; set; }
    }
}
