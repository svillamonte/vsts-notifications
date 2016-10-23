using VstsNotifications.Services.Models;
using VstsNotifications.Webhooks.Models;

namespace VstsNotifications.Webhooks.Interfaces
{
    public interface ICollaboratorMapper
    {
        Collaborator MapCollaborator(Contributor contributor);
    }
}
