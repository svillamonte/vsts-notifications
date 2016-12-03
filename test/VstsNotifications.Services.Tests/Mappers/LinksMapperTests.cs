using System;
using Xunit;
using VstsNotifications.Models.Payloads;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Mappers;

namespace VstsNotifications.Services.Tests.Mappers
{
    public class LinksMapperTests
    {
        private readonly ILinksMapper _linksMapper;

        public LinksMapperTests()
        {
            _linksMapper = new LinksMapper();
        }

        [Fact]
        public void GetWebUrlWithNoWebUrlReturnsNull()
        {
            // Arrange
            var links = new Links();

            // Act
            var url = _linksMapper.GetWebUrl(links);

            // Assert
            Assert.Null(url);
        }

        [Fact]
        public void GetWebUrlWithPopulatedWebUrlReturnsUrl()
        {
            // Arrange
            var link = new Link { Url = new Uri("http://some.url.com") };
            var links = new Links { Web = link };

            // Act
            var url = _linksMapper.GetWebUrl(links);

            // Assert
            Assert.Equal(link.Url, url);
        }
    }
}
