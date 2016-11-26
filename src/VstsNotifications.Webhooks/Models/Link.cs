using System;
using Newtonsoft.Json;

namespace VstsNotifications.Webhooks.Models
{
    public class Link
    {
        [JsonProperty("href")]
        public Uri Url { get; set; }
    }
}