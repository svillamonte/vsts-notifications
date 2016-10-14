using VstsNotifications.Entities;

namespace VstsNotifications.Webhooks.Properties
{
    public class Settings
    {
        public string SlackChannel { get; set; }
        
        public Contributor[] Contributors { get; set; }
    }
}
