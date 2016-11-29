using VstsNotifications.Models.Payloads;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Webhooks.Interfaces
{
    public interface ICollaboratorMapper
    {
        Collaborator MapCollaborator(Contributor contributor);
    }
}
