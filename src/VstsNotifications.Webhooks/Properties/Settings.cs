using VstsNotifications.Entities;

namespace VstsNotifications.Webhooks.Properties
{
    public class Settings
    {
        public string SlackWebhookUrl { get; set; }
        
        public Contributor[] Contributors { get; set; }
    }
}
