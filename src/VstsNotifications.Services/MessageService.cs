using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services
{
    public class MessageService : IMessageService
    {
        private readonly IPullRequestMessageService _pullRequestMessageService;
        private readonly ISlackMessagePayloadService _slackMessagePayloadService;
        private readonly ISlackClient _slackClient;

        public MessageService(IPullRequestMessageService pullRequestMessageService, ISlackMessagePayloadService slackMessagePayloadService, ISlackClient slackClient)
        {
            _pullRequestMessageService = pullRequestMessageService;
            _slackMessagePayloadService = slackMessagePayloadService;
            _slackClient = slackClient;
        }

        public bool NotifyReviewers(Message message)
        {
            try 
            {
                var pullRequestInfo = message.PullRequestInfo;
                var pullRequestMessages = _pullRequestMessageService.CreatePullRequestMessages(pullRequestInfo, message.Contributors);

                foreach (var pullRequestMessage in pullRequestMessages)
                {
                    var payload = _slackMessagePayloadService.CreateSlackMessagePayload(pullRequestMessage);
                    _slackClient.PostMessage(payload, message.SlackWebhookUrl.OriginalString);                
                }

                return true;
            }
            catch
            {
                return false;
            }            
        }
    }
}
