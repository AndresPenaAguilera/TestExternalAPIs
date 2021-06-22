using Code;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class RequestHttpClientUnitTest
    {
        IConsumer _IConsimudor;

        [TestMethod]
        public async Task TestMethod1()
        {
            var content = new
            {
                code = 200,
                description = "Request received",
            };

            var requestMessage = MockHttpResponseMessage(HttpStatusCode.Accepted, content);

            Initialize(new HttpMessageHandlerTest(message => Task.FromResult(requestMessage)));

            HttpResponseMessage response = await _IConsimudor.Send();

            Assert.AreEqual(content.code, (int)response.StatusCode);
            Assert.AreEqual(HttpMessageHandlerTest.RESPONSE_SERVER, response.Content.ReadAsStringAsync().Result);
        }

        private void Initialize(HttpMessageHandler messageHandler)
        {
            Code.HttpClientHandler client = new Code.HttpClientHandler(new HttpClient(messageHandler));
           
            var services = new ServiceCollection();
            services.AddScoped<IConsumer, Consumer>();
            services.AddScoped((X) => (Code.HttpClientHandler)client);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _IConsimudor = serviceProvider.GetService<IConsumer>();
        }

        private HttpResponseMessage MockHttpResponseMessage(HttpStatusCode httpStatusCode, object content)
        {
            return new HttpResponseMessage()
            {
                StatusCode = httpStatusCode,
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8,
                MediaTypeNames.Application.Json)
            };
        }
    }
}
