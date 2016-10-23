using VstsNotifications.Services.Models;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Models;

namespace VstsNotifications.Webhooks.Mappers
{
    public class CollaboratorMapper : ICollaboratorMapper
    {
        public Collaborator MapCollaborator(Contributor contributor)
        {
            if (contributor == null)
            {
                return null;
            }

            return new Collaborator
            {
                UniqueName = contributor.UniqueName,
                DisplayName = contributor.DisplayName
            };
        }
    }
}