using System.Net.Http;
using System.Threading.Tasks;

namespace Code
{
    public interface IConsumer
    {
        Task<HttpResponseMessage> Send();
    }
}
