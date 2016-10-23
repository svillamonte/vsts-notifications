using System;
using Moq;
using Xunit;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Mappers;
using VstsNotifications.Webhooks.Models;
using VstsNotifications.Webhooks.Models.PullRequest;
using VstsNotifications.Services.Models;
using System.Collections.Generic;

namespace VstsNotifications.Webhooks.Tests.Mappers
{
    public class PullRequestInfoMapperTests
    {
        private readonly Mock<ICollaboratorMapper> _mockCollaboratorMapper;
        private readonly IPullRequestInfoMapper _pullRequestInfoMapper;

        public PullRequestInfoMapperTests()
        {
            _mockCollaboratorMapper = new Mock<ICollaboratorMapper>();
            _pullRequestInfoMapper = new PullRequestInfoMapper(_mockCollaboratorMapper.Object);
        }

        [Fact]
        public void MapPullRequestInfoWithNullPayloadReturnsNull()
        {
            // Arrange
            var pullRequestPayload = (PullRequestPayload) null;

            // Act
            var pullRequestInfo = _pullRequestInfoMapper.MapPullRequestInfo(pullRequestPayload);

            // Assert
            Assert.Null(pullRequestInfo);
        }

        [Fact]
        public void MapPullRequestInfoWithNullResourceReturnsNull()
        {
            // Arrange
            var pullRequestPayload = new PullRequestPayload();

            // Act
            var pullRequestInfo = _pullRequestInfoMapper.MapPullRequestInfo(pullRequestPayload);

            // Assert
            Assert.Null(pullRequestInfo);
        }

        [Fact]
        public void MapPullRequestInfoWithResourceInstantiatedButNotPopulatedReturnsInstanceWithoutValues()
        {
            // Arrange
            var pullRequestResource = new PullRequestResource();
            var pullRequestPayload = new PullRequestPayload { Resource = pullRequestResource };

            _mockCollaboratorMapper
                .Setup(x => x.MapCollaborator(pullRequestResource.CreatedBy))
                .Returns((Collaborator) null);

            // Act
            var pullRequestInfo = _pullRequestInfoMapper.MapPullRequestInfo(pullRequestPayload);

            // Assert
            Assert.NotNull(pullRequestInfo);
            Assert.Null(pullRequestInfo.Url);
            Assert.Null(pullRequestInfo.Author);
            Assert.Empty(pullRequestInfo.Reviewers);
        }

        [Fact]
        public void MapPullRequestInfoWithResourcePopulatedReturnsInfoValues()
        {
            // Arrange
            var creator = new Contributor { UniqueName = "unique", DisplayName = "display" };

            var pullRequestResource = new PullRequestResource 
            {
                Url = new Uri("https://wwww.myurl.com"),
                CreatedBy = creator
            };

            var pullRequestPayload = new PullRequestPayload { Resource = pullRequestResource };

            _mockCollaboratorMapper
                .Setup(x => x.MapCollaborator(pullRequestResource.CreatedBy))
                .Returns(new Collaborator { UniqueName = creator.UniqueName, DisplayName = creator.DisplayName });

            // Act
            var pullRequestInfo = _pullRequestInfoMapper.MapPullRequestInfo(pullRequestPayload);

            // Assert
            Assert.NotNull(pullRequestInfo);
            Assert.Equal(pullRequestResource.Url, pullRequestInfo.Url);
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

            var pullRequestPayload = new PullRequestPayload { Resource = pullRequestResource };

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
            var pullRequestInfo = _pullRequestInfoMapper.MapPullRequestInfo(pullRequestPayload);
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
