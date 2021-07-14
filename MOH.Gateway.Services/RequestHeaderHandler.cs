using Microsoft.AspNetCore.Http;
using MOH.Gateway.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MOH.Gateway.Services
{
    public class RequestHeaderHandler : IRequestHeaderHandler
    {
        /// <summary>
        /// Reads Incoming request http header valus related to actual host
        /// </summary>
        /// <param name="headercollection"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetFormattedHeaders(IHeaderDictionary headercollection)
        {
            //var headers = new Dictionary<string, string>();
            //foreach (var header in headercollection)
            //{
            //    headers.Add(header.Key, header.Value);
            //}
            return headercollection.ToDictionary(x => x.Key, x => x.Value.ToString());
        }
        //public Dictionary<string, string> GetFormattedHeaders(UserDTO userdto)
        //{
        //    var headers = new Dictionary<string, string>();
        //    headers.Add("abb.iama._useremail", userdto.Email);
        //    headers.Add("abb.iama._username", userdto.Name);
        //    return headers;
        //}
    }
}
