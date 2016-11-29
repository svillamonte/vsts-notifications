using Newtonsoft.Json;

namespace VstsNotifications.Models.Payloads.PullRequest
{
    public class PullRequestPayload : BasePayload
    {
        [JsonProperty("resource")]
        public PullRequestResource Resource { get; set; }
    }
}
