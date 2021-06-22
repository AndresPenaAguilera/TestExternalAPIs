using System.Net.Http;
using System.Threading.Tasks;

namespace Code
{
    public class AppHttpClient
    {
        
        public AppHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> Send(HttpMethod method, HttpContent body = null)
        {
            var httpRequestMessage = new HttpRequestMessage(method, $"http://localhost/api/method")
            {
                Content = body
            };

            return await _httpClient.SendAsync(httpRequestMessage);
        }

        public HttpClient _httpClient { get; }
    }
}