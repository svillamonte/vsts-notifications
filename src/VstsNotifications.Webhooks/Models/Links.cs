using Newtonsoft.Json;

namespace VstsNotifications.Webhooks.Models
{
    public class Links
    {
        public Links()
        {
            Web = new Link();        
        }
        
        [JsonProperty("web")]
        public Link Web { get; set; }
    }
}