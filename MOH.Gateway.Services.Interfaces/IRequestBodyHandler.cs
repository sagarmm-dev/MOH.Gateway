using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace MOH.Gateway.Services.Interfaces
{
    public interface IRequestBodyHandler
    {
        Task<StringContent> GetFormattedBody(HttpRequest httpRequest);
    }
}
