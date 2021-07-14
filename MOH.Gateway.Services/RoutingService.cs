using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOH.Gateway.Common;
using MOH.Gateway.Services.Interfaces;
using MOH.Gateway.Services.Model;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace MOH.Gateway.Services
{
    public class RoutingService : IRoutingService
    {
        private readonly IRequestHeaderHandler _requestheaderhandler;
        private readonly IRequestBodyHandler _requestbodyhandler;
        private readonly IRequestUriHandler _requesturihandler;
        private readonly IHttpHelper _httpHelper;

        private readonly IOptions<ApiService> _apiService;

        public RoutingService(IRequestHeaderHandler requestheaderhandler,
            IRequestBodyHandler requestbodyhandler, IHttpHelper httpHelper, IOptions<ApiService> apiService, IRequestUriHandler requesturihandler)
        {
            _requestheaderhandler = requestheaderhandler;
            _requestbodyhandler = requestbodyhandler;
            _httpHelper = httpHelper;
            _apiService = apiService;
            _requesturihandler = requesturihandler;
        }

        public async Task<HttpResponseMessage> RouteAsync(HttpContext httpcontext)
        {
            var hosturi = _requesturihandler.GetFormattedUrl(_apiService.Value.BaseUrl, httpcontext.Request);
            var body = await _requestbodyhandler.GetFormattedBody(httpcontext.Request);
            var headers = _requestheaderhandler.GetFormattedHeaders(httpcontext.Request.Headers);
            var method = httpcontext.Request.Method;

            if (method == "POST")
                return await _httpHelper.Post(hosturi, headers, body);
            else if (method == "GET")
                return await _httpHelper.Get(hosturi, headers);
            else if (method == "DELETE")
                return await _httpHelper.Delete(hosturi, headers);
            else if (method == "PUT")
                return await _httpHelper.Put(hosturi, headers, body);

            throw new Exception("Http action not implemented");
        }

    }
}
