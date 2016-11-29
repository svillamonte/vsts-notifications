using System;
using VstsNotifications.Models.Payloads.PullRequest;
using VstsNotifications.Services.Models;
using VstsNotifications.Webhooks.Interfaces;
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
            try 
            {
                return new Message 
                { 
                    SlackWebhookUrl = new Uri(settings.SlackWebhookUrl),
                    Contributors = settings.Contributors,
                    PullRequestInfo = _pullRequestInfoMapper.MapPullRequestInfo(pullRequestPayload.Resource) 
                };
            }
            catch
            {
                return new Message();
            }            
        }
    }
}
