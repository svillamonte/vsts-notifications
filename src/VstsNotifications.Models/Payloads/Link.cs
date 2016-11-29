using System;
using Newtonsoft.Json;

namespace VstsNotifications.Models.Payloads
{
    public class Link
    {
        [JsonProperty("href")]
        public Uri Url { get; set; }
    }
}