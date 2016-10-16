using System.Net.Http;
using System.Threading.Tasks;
using VstsNotifications.Services.Interfaces;

namespace VstsNotifications.Services.Wrappers
{
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return await _httpClient.PostAsync(requestUri, content);
        }
    }
}
