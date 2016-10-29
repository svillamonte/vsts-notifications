using System.Collections.Generic;
using Newtonsoft.Json;

namespace VstsNotifications.Services.Models
{
    public class SlackMessagePayload
    {
        public SlackMessagePayload ()
        {
            Attachments = new List<SlackAttachment>();          
        }

        [JsonProperty("username")]
        public string Username { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("attachments")]
        public IList<SlackAttachment> Attachments { get; set; }
    }
}
