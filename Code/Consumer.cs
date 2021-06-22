using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class Consumer : IConsumer
    {
        private AppHttpClient _appHttpClient;

        public Consumer(AppHttpClient AppHttpClient) 
        {
            _appHttpClient = AppHttpClient;
        }

        public Task<HttpResponseMessage> Send()
        {
            var content = new
            {
                code = 200,
                description = "Request",
            };

            HttpContent stringContent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");

            return _appHttpClient.Send(HttpMethod.Post, stringContent);
         }
    }
}
