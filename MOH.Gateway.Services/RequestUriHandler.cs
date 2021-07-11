using Microsoft.AspNetCore.Http;
using MOH.Gateway.Common;
using MOH.Gateway.Services.Interfaces;
using System;

namespace MOH.Gateway.Services
{
    public class RequestUriHandler: IRequestUriHandler
    {
        /// <summary>
        /// Formats the uri with request data
        /// </summary>
        /// <param name="routeentry"></param>
        /// <param name="httpcontext"></param>
        /// <returns>Returns Uri</returns>
        public Uri GetFormattedUrl(string routeentry, HttpRequest httprequest)
        {
            var uri = new Uri($"{routeentry.Trim()}{httprequest.Path.Value.TrimStart('/') }{httprequest.QueryString.Value.Trim()}");
            if (!uri.IsWellFormedOriginalString())
                throw new UriFormatException(Constants.GATEWAY_INVALID_HOST_URI);
            return uri;
        }
    }
}
