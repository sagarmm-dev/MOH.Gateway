using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MOH.Gateway.Services.Interfaces
{
    public interface IRoutingService
    {
        Task<HttpResponseMessage> RouteAsync(HttpContext httpcontext);
    }
}
