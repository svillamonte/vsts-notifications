using VstsNotifications.Services.Models;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Models.PullRequest;

namespace VstsNotifications.Webhooks.Mappers
{
    public class PullRequestInfoMapper : IPullRequestInfoMapper
    {
        private readonly ICollaboratorMapper _collaboratorMapper;

        public PullRequestInfoMapper (ICollaboratorMapper collaboratorMapper)
        {
            _collaboratorMapper = collaboratorMapper;          
        }

        public PullRequestInfo MapPullRequestInfo(PullRequestPayload pullRequestPayload)
        {
            if (pullRequestPayload == null || pullRequestPayload.Resource == null)
            {
                return null;
            }

            var pullRequestResource = pullRequestPayload.Resource;

            var pullRequestInfo = new PullRequestInfo
            {
                Url = pullRequestResource.Url,
                Author = _collaboratorMapper.MapCollaborator(pullRequestResource.CreatedBy)
            };
            
            foreach (var reviewer in pullRequestResource.Reviewers)
            {
                pullRequestInfo.Reviewers.Add(_collaboratorMapper.MapCollaborator(reviewer));
            }

            return pullRequestInfo;
        }
    }
}
