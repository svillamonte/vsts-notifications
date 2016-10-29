using Newtonsoft.Json;

namespace VstsNotifications.Services.Models
{
    public class SlackAttachment
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
