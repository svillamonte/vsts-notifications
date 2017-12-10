using System;
using System.Linq;
using Xunit;
using VstsNotifications.Models;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Tests
{
    public class PullRequestMessageServiceTests
    {
        private readonly IPullRequestMessageService _pullRequestMessageService;

        public PullRequestMessageServiceTests()
        {
            _pullRequestMessageService = new PullRequestMessageService();
        }

        [Fact]
        public void CreatePullRequestMessagesWithPullRequestInfoNullThrowsNullReferenceException()
        {
            // Arrange
            var contributorOne = new Contributor { Id = "idone", SlackUserId = "shone" };
            var contributorTwo = new Contributor { Id = "idtwo", SlackUserId = "shtwo" };
            
            var pullRequestInfo = (PullRequestInfo) null;
            var contributorsInfo = new [] { contributorOne, contributorTwo };

            // Act

            // Assert
            Assert.Throws<NullReferenceException>(() => _pullRequestMessageService.CreatePullRequestMessages(pullRequestInfo, contributorsInfo));
        }
        
        [Fact]
        public void CreatePullRequestMessagesWithoutReviewersAndEmptyContributorsReturnsEmpty()
        {
            // Arrange
            var contributorOne = new Contributor { Id = "idone", SlackUserId = "shone" };
            var contributorTwo = new Contributor { Id = "idtwo", SlackUserId = "shtwo" };
            
            var pullRequestInfo = new PullRequestInfo();
            var contributorsInfo = new Contributor[0];

            // Act
            var pullRequestMessages = _pullRequestMessageService.CreatePullRequestMessages(pullRequestInfo, contributorsInfo);

            // Assert
            Assert.NotNull(pullRequestMessages);
            Assert.Empty(pullRequestMessages);
        }
        
        [Fact]
        public void CreatePullRequestMessagesWithoutReviewersReturnsEmpty()
        {
            // Arrange
            var contributorOne = new Contributor { Id = "idone", SlackUserId = "shone" };
            var contributorTwo = new Contributor { Id = "idtwo", SlackUserId = "shtwo" };
            
            var pullRequestInfo = new PullRequestInfo();
            var contributorsInfo = new [] { contributorOne, contributorTwo };

            // Act
            var pullRequestMessages = _pullRequestMessageService.CreatePullRequestMessages(pullRequestInfo, contributorsInfo);

            // Assert
            Assert.NotNull(pullRequestMessages);
            Assert.Empty(pullRequestMessages);
        }

        [Fact]
        public void CreatePullRequestMessagesWithAllParametersReturnsPullRequestMessages()
        {
            // Arrange
            var contributorOne = new Contributor { Id = "two@collaborator.com", SlackUserId = "shone" };
            var contributorTwo = new Contributor { Id = "three@collaborator.com", SlackUserId = "shtwo" };

            var author = new Collaborator { UniqueName = "one@collaborator.com", DisplayName = "Collaborator one" };

            var reviewerOne = new Collaborator { UniqueName = "two@collaborator.com", DisplayName = "Collaborator two" };
            var reviewerTwo = new Collaborator { UniqueName = "three@collaborator.com", DisplayName = "Collaborator three" };

            var pullRequestInfo = new PullRequestInfo
            {
                Url = new Uri("http://my.pullrequest.com"),
                Author = author,
            };
            pullRequestInfo.Reviewers.Add(reviewerOne);
            pullRequestInfo.Reviewers.Add(reviewerTwo);

            var contributorsInfo = new [] { contributorOne, contributorTwo };

            // Act
            var pullRequestMessages = _pullRequestMessageService.CreatePullRequestMessages(pullRequestInfo, contributorsInfo);

            // Assert
            Assert.NotNull(pullRequestMessages);
            Assert.Single(pullRequestMessages);

            Assert.Equal(contributorOne.SlackUserId, pullRequestMessages.ElementAt(0).ReviewersSlackUserId.ElementAt(0));
            Assert.Equal(contributorTwo.SlackUserId, pullRequestMessages.ElementAt(0).ReviewersSlackUserId.ElementAt(1));            
            Assert.Equal(author.DisplayName, pullRequestMessages.ElementAt(0).AuthorDisplayName);
            Assert.Equal(pullRequestInfo.Url, pullRequestMessages.ElementAt(0).PullRequestUrl);
        }

        [Fact]
        public void CreatePullRequestMessagesWithMissingContributorReturnsPullRequestMessagesForExistingOnes()
        {
            // Arrange
            var contributorOne = new Contributor { Id = "two@collaborator.com", SlackUserId = "shone" };
            var contributorTwo = new Contributor { Id = "four@collaborator.com", SlackUserId = "shfour" };

            var author = new Collaborator { UniqueName = "one@collaborator.com", DisplayName = "Collaborator one" };

            var reviewerOne = new Collaborator { UniqueName = "two@collaborator.com", DisplayName = "Collaborator two" };
            var reviewerTwo = new Collaborator { UniqueName = "three@collaborator.com", DisplayName = "Collaborator three" };

            var pullRequestInfo = new PullRequestInfo
            {
                Url = new Uri("http://my.pullrequest.com"),
                Author = author,
            };
            pullRequestInfo.Reviewers.Add(reviewerOne);
            pullRequestInfo.Reviewers.Add(reviewerTwo);

            var contributorsInfo = new [] { contributorOne, contributorTwo };

            // Act
            var pullRequestMessages = _pullRequestMessageService.CreatePullRequestMessages(pullRequestInfo, contributorsInfo);

            // Assert
            Assert.NotNull(pullRequestMessages);
            Assert.Single(pullRequestMessages);
            Assert.Single(pullRequestMessages.ElementAt(0).ReviewersSlackUserId);

            Assert.Equal(contributorOne.SlackUserId, pullRequestMessages.ElementAt(0).ReviewersSlackUserId.ElementAt(0));
            Assert.Equal(author.DisplayName, pullRequestMessages.ElementAt(0).AuthorDisplayName);
            Assert.Equal(pullRequestInfo.Url, pullRequestMessages.ElementAt(0).PullRequestUrl);
        }
    }
}
