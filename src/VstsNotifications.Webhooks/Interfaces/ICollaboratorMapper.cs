using VstsNotifications.Services.Models;
using VstsNotifications.Models.Payloads;

namespace VstsNotifications.Webhooks.Interfaces
{
    public interface ICollaboratorMapper
    {
        Collaborator MapCollaborator(Contributor contributor);
    }
}
