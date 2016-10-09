using Newtonsoft.Json;

namespace VstsNotifications.Webhooks.Models.PullRequest
{
    public class PullRequestPayload : BasePayload
    {
        [JsonProperty("resource")]
        public PullRequestResource Resource { get; set; }
    }
}
