using System;
using Xunit;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Mappers;
using VstsNotifications.Webhooks.Models;

namespace VstsNotifications.Webhooks.Tests.Mappers
{
    public class CollaboratorMapperTests
    {
        private readonly ICollaboratorMapper _collaboratorMapper;

        public CollaboratorMapperTests()
        {
            _collaboratorMapper = new CollaboratorMapper();
        }

        [Fact]
        public void MapCollaboratorWithContributorNullReturnsNull()
        {
            // Arrange
            var contributor = (Contributor) null;

            // Act
            var collaborator = _collaboratorMapper.MapCollaborator(contributor);

            // Assert
            Assert.Null(collaborator);
        }

        [Fact]
        public void MapCollaboratorWithContributorNotNullButNotPopulatedReturnsEmptyCollaborator()
        {
            // Arrange
            var contributor = new Contributor();

            // Act
            var collaborator = _collaboratorMapper.MapCollaborator(contributor);

            // Assert
            Assert.NotNull(collaborator);
            Assert.Null(collaborator.UniqueName);
            Assert.Null(collaborator.DisplayName);
        }

        [Fact]
        public void MapCollaboratorWithExpectedAttributesPopulatedReturnsCollaboratorWithExpectedValues()
        {
            // Arrange
            var contributor = new Contributor 
            {
                Id = "id",
                DisplayName = "display name",
                UniqueName = "unique name",
                Url = new Uri("https://www.myurl.com"),
                ImageUrl = new Uri("https://www.myimage.com")
            };

            // Act
            var collaborator = _collaboratorMapper.MapCollaborator(contributor);

            // Assert
            Assert.NotNull(collaborator);
            Assert.Equal(contributor.UniqueName, collaborator.UniqueName);
            Assert.Equal(contributor.DisplayName, collaborator.DisplayName);
        }
    }
}