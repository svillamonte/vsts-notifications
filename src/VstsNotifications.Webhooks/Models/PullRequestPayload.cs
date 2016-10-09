using Newtonsoft.Json;

namespace VstsNotifications.Webhooks.Models
{
    public class PullRequestPayload : BasePayload
    {
        [JsonProperty("resource")]
        public PullRequestResource Resource { get; set; }
    }
}
