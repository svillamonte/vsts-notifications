using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services
{
    public class SlackClient : ISlackClient
    {
        private readonly IHttpClient _httpClient;

        public SlackClient(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> PostMessageAsync(SlackMessagePayload slackMessage, string webhookUrl)
        {
            try 
            {
                var jsonContent = JsonConvert.SerializeObject(slackMessage);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(webhookUrl, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
