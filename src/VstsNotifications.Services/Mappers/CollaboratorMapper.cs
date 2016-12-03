using VstsNotifications.Models.Payloads;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Mappers
{
    public class CollaboratorMapper : ICollaboratorMapper
    {
        public Collaborator MapCollaborator(Contributor contributor)
        {
            return new Collaborator
            {
                UniqueName = contributor.UniqueName,
                DisplayName = contributor.DisplayName
            };
        }
    }
}
