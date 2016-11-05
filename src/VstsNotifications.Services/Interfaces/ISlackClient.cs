using System.Threading.Tasks;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Interfaces
{
    public interface ISlackClient
    {
        Task<bool> PostMessageAsync(SlackMessagePayload slackMessage, string webhookUrl);
    }
}