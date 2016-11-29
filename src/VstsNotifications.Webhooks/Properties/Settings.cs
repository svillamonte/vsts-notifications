using VstsNotifications.Models;

namespace VstsNotifications.Webhooks.Properties
{
    public class Settings
    {
        public Settings()
        {
            Contributors = new Contributor[0];
        }

        public string SlackWebhookUrl { get; set; }
        
        public Contributor[] Contributors { get; set; }
    }
}
