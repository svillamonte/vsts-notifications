using Newtonsoft.Json;

namespace VstsNotifications.Services.Models
{
    public class SlackMessagePayload
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
