using System;
using Newtonsoft.Json;

namespace VstsNotifications.Models.Payloads
{
    public class Commit
    {
        [JsonProperty("commitId")]
        public string CommitId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
