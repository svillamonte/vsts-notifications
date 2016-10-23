using System;
using VstsNotifications.Services.Models;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Models.PullRequest;
using VstsNotifications.Webhooks.Properties;

namespace VstsNotifications.Webhooks.Mappers
{
    public class MessageMapper : IMessageMapper
    {
        private readonly IPullRequestInfoMapper _pullRequestInfoMapper;

        public MessageMapper (IPullRequestInfoMapper pullRequestInfoMapper)
        {
            _pullRequestInfoMapper = pullRequestInfoMapper;          
        }

        public Message MapMessage(PullRequestPayload pullRequestPayload, Settings settings)
        {
            if (pullRequestPayload == null || settings == null)
            {
                return null;
            }

            return new Message 
            { 
                SlackWebhookUrl = settings.SlackWebhookUrl != null ? new Uri(settings.SlackWebhookUrl) : null,
                Contributors = settings.Contributors,
                PullRequestInfo = _pullRequestInfoMapper.MapPullRequestInfo(pullRequestPayload) 
            };
        }
    }
}
