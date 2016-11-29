using Newtonsoft.Json;

namespace VstsNotifications.Models.Payloads
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