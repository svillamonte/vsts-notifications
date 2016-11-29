using System;
using Newtonsoft.Json;

namespace VstsNotifications.Models.Payloads
{
    public class Repository
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("remoteUrl")]
        public Uri RemoteUrl { get; set; }
    }
}
