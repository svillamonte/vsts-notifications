using System;
using Newtonsoft.Json;

namespace VstsNotifications.Models.Payloads
{
    public abstract class BasePayload
    {
        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }

        [JsonProperty("notificationId")]
        public int NotificationId {get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("publisherId")]
        public string PublisherId { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("detailedMessage")]
        public Message DetailedMessage { get; set; }

        [JsonProperty("resourceVersion")]
        public string ResourceVersion { get; set; }

        [JsonProperty("resourceContainers")]
        public ResourceContainers ResourceContainers { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
