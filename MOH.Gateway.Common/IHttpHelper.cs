using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOH.Gateway.Common
{
    public interface IHttpHelper
    {
        Task<HttpResponseMessage> Post(Uri uri, IDictionary<string, string> headers, HttpContent body);
        Task<HttpResponseMessage> Get(Uri uri, IDictionary<string, string> headers);
        Task<HttpResponseMessage> Put(Uri uri, IDictionary<string, string> headers, HttpContent body);
        Task<HttpResponseMessage> Delete(Uri uri, IDictionary<string, string> headers);
    }
}
