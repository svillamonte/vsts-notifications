using System;
using Newtonsoft.Json;

namespace VstsNotifications.Webhooks.Models
{
    public class Container
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }
    }
}
