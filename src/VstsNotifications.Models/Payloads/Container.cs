using System;
using Newtonsoft.Json;

namespace VstsNotifications.Models.Payloads
{
    public class Container
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }
    }
}
