using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MOH.Gateway.Services.Interfaces
{
    public interface IRequestHeaderHandler
    {
        Dictionary<string, string> GetFormattedHeaders(IHeaderDictionary headercollection);
        //Dictionary<string, string> GetFormattedHeaders(UserDTO userdto);
    }
}
