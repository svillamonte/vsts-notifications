using System.Net.Http;
using System.Threading.Tasks;

namespace VstsNotifications.Services.Interfaces
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}