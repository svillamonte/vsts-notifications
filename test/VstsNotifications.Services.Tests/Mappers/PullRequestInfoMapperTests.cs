using System;
using System.Collections.Generic;
using Moq;
using Xunit;
using VstsNotifications.Models.Payloads;
using VstsNotifications.Models.Payloads.PullRequest;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Mappers;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Webhooks.Tests.Mappers
{
    public class PullRequestInfoMapperTests
    {
        private readonly Mock<ILinksMapper> _mockLinksMapper;
        private readonly Mock<ICollaboratorMapper> _mockCollaboratorMapper;
        private readonly IPullRequestInfoMapper _pullRequestInfoMapper;

        public PullRequestInfoMapperTests()
        {
            _mockCollaboratorMapper = new Mock<ICollaboratorMapper>();
            _mockLinksMapper = new Mock<ILinksMapper>();

            _pullRequestInfoMapper = new PullRequestInfoMapper(_mockLinksMapper.Object, _mockCollaboratorMapper.Object);
        }

        [Fact]
        public void MapPullRequestInfoWithNullPayloadReturnsNull()
        {
            // Arrange
            var pullRequestResource = (PullRequestResource) null;

            // Act
            
            // Assert
            Assert.Throws<NullReferenceException>(() => _pullRequestInfoMapper.MapPullRequestInfo(pullRequestResource));
        }

        [Fact]
        public void MapPullRequestInfoWithResourceInstantiatedButNotPopulatedReturnsInstanceWithoutValues()
        {
            // Arrange
            var pullRequestResource = new PullRequestResource();

            _mockLinksMapper
                .Setup(x => x.GetWebUrl(pullRequestResource.Links))
                .Returns(pullRequestResource.Links.Web.Url);

            _mockCollaboratorMapper
                .Setup(x => x.MapCollaborator(pullRequestResource.CreatedBy))
                .Returns(new Collaborator());
            
            // Act
            var pullRequestInfo = _pullRequestInfoMapper.MapPullRequestInfo(pullRequestResource);

            // Assert
            Assert.NotNull(pullRequestInfo);
            Assert.Null(pullRequestInfo.Url);
            Assert.Null(pullRequestInfo.Author.DisplayName);
            Assert.Null(pullRequestInfo.Author.UniqueName);
            Assert.Empty(pullRequestInfo.Reviewers);
        }

        [Fact]
        public void MapPullRequestInfoWithResourcePopulatedReturnsInfoValues()
        {
            // Arrange
            var pullRequestResource = new PullRequestResource 
            {
                Links = new Links { Web = new Link { Url = new Uri("https://wwww.myurl.com") } },
                CreatedBy = new Contributor { UniqueName = "unique", DisplayName = "display" }
            };

            _mockLinksMapper
                .Setup(x => x.GetWebUrl(pullRequestResource.Links))
                .Returns(pullRequestResource.Links.Web.Url);

            _mockCollaboratorMapper
                .Setup(x => x.MapCollaborator(pullRequestResource.CreatedBy))
                .Returns(new Collaborator { UniqueName = "unique", DisplayName = "display" });

            // Act
            var pullRequestInfo = _pullRequestInfoMapper.MapPullRequestInfo(pullRequestResource);

            // Assert
            Assert.NotNull(pullRequestInfo);
            Assert.Equal(pullRequestResource.Links.Web.Url, pullRequestInfo.Url);
            Assert.Equal(pullRequestResource.CreatedBy.UniqueName, pullRequestInfo.Author.UniqueName);
            Assert.Equal(pullRequestResource.CreatedBy.DisplayName, pullRequestInfo.Author.DisplayName);
            Assert.Empty(pullRequestInfo.Reviewers);
        }

        [Fact]
        public void MapPullRequestInfoWithResourceReviewersPopulatedReturnsInfoWithReviewers()
        {
            // Arrange
            var reviewerOne = new Reviewer { UniqueName = "uone", DisplayName = "done" };
            var reviewerTwo = new Reviewer { UniqueName = "utwo", DisplayName = "dtwo" };
            var reviewerThree = new Reviewer { UniqueName = "uthree", DisplayName = "dthree" };
            
            var pullRequestResource = new PullRequestResource();
            pullRequestResource.Reviewers.Add(reviewerOne);
            pullRequestResource.Reviewers.Add(reviewerTwo);
            pullRequestResource.Reviewers.Add(reviewerThree);

            _mockCollaboratorMapper
                .Setup(x => x.MapCollaborator(pullRequestResource.CreatedBy))
                .Returns((Collaborator) null);
            _mockCollaboratorMapper
                .Setup(x => x.MapCollaborator(reviewerOne))
                .Returns(new Collaborator { UniqueName = reviewerOne.UniqueName, DisplayName = reviewerOne.DisplayName });
            _mockCollaboratorMapper
                .Setup(x => x.MapCollaborator(reviewerTwo))
                .Returns(new Collaborator { UniqueName = reviewerTwo.UniqueName, DisplayName = reviewerTwo.DisplayName });
            _mockCollaboratorMapper
                .Setup(x => x.MapCollaborator(reviewerThree))
                .Returns(new Collaborator { UniqueName = reviewerThree.UniqueName, DisplayName = reviewerThree.DisplayName });

            // Act
            var pullRequestInfo = _pullRequestInfoMapper.MapPullRequestInfo(pullRequestResource);
            var reviewers = pullRequestInfo.Reviewers as List<Reviewer>;

            // Assert
            Assert.NotNull(pullRequestInfo);
            Assert.NotEmpty(pullRequestInfo.Reviewers);
            Assert.Equal(3, pullRequestInfo.Reviewers.Count);
            
            Assert.Equal(reviewerOne.UniqueName, pullRequestInfo.Reviewers[0].UniqueName);
            Assert.Equal(reviewerOne.DisplayName, pullRequestInfo.Reviewers[0].DisplayName);
            
            Assert.Equal(reviewerTwo.UniqueName, pullRequestInfo.Reviewers[1].UniqueName);
            Assert.Equal(reviewerTwo.DisplayName, pullRequestInfo.Reviewers[1].DisplayName);
            
            Assert.Equal(reviewerThree.UniqueName, pullRequestInfo.Reviewers[2].UniqueName);
            Assert.Equal(reviewerThree.DisplayName, pullRequestInfo.Reviewers[2].DisplayName);
        }
    }
}
