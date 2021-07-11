using Microsoft.AspNetCore.Http;
using System;

namespace MOH.Gateway.Services.Interfaces
{
    public interface IRequestUriHandler
    {
        Uri GetFormattedUrl(string routeentry, HttpRequest httprequest);
    }
}
