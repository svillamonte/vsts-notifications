using System;
using Newtonsoft.Json;

namespace VstsNotifications.Webhooks.Models
{
    public class Reviewer : Contributor
    {
        [JsonProperty("reviewerUrl")]
        public Uri ReviewerUrl { get; set; }

        [JsonProperty("vote")]
        public int Vote { get; set; }
    }
}
