using System;
using Xunit;
using VstsNotifications.Services;
using VstsNotifications.Services.Models;
using VstsNotifications.Services.Wrappers;

namespace VstsNotifications.Tests.Services
{
    public class Tests
    {
        [Fact]
        public void Test1() 
        {
            var url = "https://hooks.slack.com/services/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";
            var httpClient = new HttpClientWrapper();
            var payload = new SlackMessagePayload
            {
                Username = "toqueton",
                Text = "Que kayak"
            };
            var slackClient = new SlackClient(httpClient);
            var response = slackClient.PostMessage(payload, url);

            Console.WriteLine(response.Result);

            Assert.True(true);
        }
    }
}
