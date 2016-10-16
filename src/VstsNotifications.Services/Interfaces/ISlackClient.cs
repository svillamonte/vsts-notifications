using System.Threading.Tasks;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Interfaces
{
    public interface ISlackClient
    {
        Task<bool> PostMessage(SlackMessagePayload slackMessage, string webhookUrl);
    }
}