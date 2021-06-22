using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Code
{
    public class HttpMessageHandlerTest : HttpMessageHandler
    {
        private Func<HttpResponseMessage, Task<HttpResponseMessage>> _configuration;
        public static string RESPONSE_SERVER = "Response server";
        public HttpMessageHandlerTest(Func<HttpResponseMessage, Task<HttpResponseMessage>> configuration)
        {
            _configuration = configuration;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(RESPONSE_SERVER, Encoding.UTF8, "application/json")
            });
        }
    }
}