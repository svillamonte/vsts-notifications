using System;
using Xunit;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;
using VstsNotifications.Entities;
using System.Linq;

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
            var pullRequestMessages = _pullRequestMessageService.CreatePullRequestMessages(pullRequestInfo, contributorsInfo);

            // Assert
            Assert.Throws<NullReferenceException>(() => pullRequestMessages.ToList());
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
            Assert.Equal(pullRequestInfo.Reviewers.Count, pullRequestMessages.Count());

            Assert.Equal(contributorOne.SlackUserId, pullRequestMessages.ElementAt(0).ReviewerSlackUserId);
            Assert.Equal(author.DisplayName, pullRequestMessages.ElementAt(0).AuthorDisplayName);
            Assert.Equal(pullRequestInfo.Url, pullRequestMessages.ElementAt(0).PullRequestUrl);

            Assert.Equal(contributorTwo.SlackUserId, pullRequestMessages.ElementAt(1).ReviewerSlackUserId);
            Assert.Equal(author.DisplayName, pullRequestMessages.ElementAt(1).AuthorDisplayName);
            Assert.Equal(pullRequestInfo.Url, pullRequestMessages.ElementAt(1).PullRequestUrl);
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
            Assert.Equal(pullRequestInfo.Reviewers.Count - 1, pullRequestMessages.Count());

            Assert.Equal(contributorOne.SlackUserId, pullRequestMessages.ElementAt(0).ReviewerSlackUserId);
            Assert.Equal(author.DisplayName, pullRequestMessages.ElementAt(0).AuthorDisplayName);
            Assert.Equal(pullRequestInfo.Url, pullRequestMessages.ElementAt(0).PullRequestUrl);
        }
    }
}
