using Newtonsoft.Json;

namespace VstsNotifications.Webhooks.Models
{
    public class ResourceContainers
    {
        [JsonProperty("collection")]
        public Container Collection { get; set; }

        [JsonProperty("account")]
        public Container Account { get; set; }

        [JsonProperty("project")]
        public Container Project { get; set; }
    }
}
