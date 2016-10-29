using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Interfaces
{
    public interface IMessageService
    {
        bool NotifyReviewers(Message message);
    }
}
