using VstsNotifications.Models.Payloads;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Interfaces
{
    public interface ICollaboratorMapper
    {
        Collaborator MapCollaborator(Contributor contributor);
    }
}
