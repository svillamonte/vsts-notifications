using VstsNotifications.Models.Payloads.PullRequest;
using VstsNotifications.Services.Models;
using VstsNotifications.Webhooks.Interfaces;

namespace VstsNotifications.Webhooks.Mappers
{
    public class PullRequestInfoMapper : IPullRequestInfoMapper
    {
        private readonly ILinksMapper _linksMapper;
        private readonly ICollaboratorMapper _collaboratorMapper;

        public PullRequestInfoMapper (ILinksMapper linksMapper, ICollaboratorMapper collaboratorMapper)
        {
            _linksMapper = linksMapper;
            _collaboratorMapper = collaboratorMapper;          
        }

        public PullRequestInfo MapPullRequestInfo(PullRequestResource pullRequestResource)
        {
            var pullRequestInfo = new PullRequestInfo
            {
                Url = _linksMapper.GetWebUrl(pullRequestResource.Links),
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
