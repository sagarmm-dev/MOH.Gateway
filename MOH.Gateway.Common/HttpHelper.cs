using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MOH.Gateway.Common
{
    public class HttpHelper : IHttpHelper
    {
        private IHttpClientFactory _httpclientfactory;
        public HttpHelper(IHttpClientFactory httpclientfactory)
        {
            _httpclientfactory = httpclientfactory;
        }
        /// <summary>
        /// Performs Http post action
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="body"></param>
        /// <returns>Returns HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> Post(Uri uri, IDictionary<string, string> headers, HttpContent body)
        {
            var client = _httpclientfactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", headers["Accept"]);
            body.Headers.Add("Content-Type", headers["Content-Type"]);
            return await client.PostAsync(uri, body);
        }

        /// <summary>
        /// Performs Http Get action
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>Returns HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> Get(Uri uri, IDictionary<string, string> headers)
        {
            var client = _httpclientfactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", headers["Accept"]);
            return await client.GetAsync(uri);
        }
        /// <summary>
        /// Performs Http Put Action
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="body"></param>
        /// <returns>Returns HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> Put(Uri uri, IDictionary<string, string> headers, HttpContent body)
        {
            var client = _httpclientfactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", headers["Accept"]);
            body.Headers.Add("Content-Type", headers["Content-Type"]);

            return await client.PutAsync(uri, body);
        }
        /// <summary>
        /// Performs Http Delete Action
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>Returns HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> Delete(Uri uri, IDictionary<string, string> headers)
        {
            var client = _httpclientfactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", headers["Accept"]);
            return await client.DeleteAsync(uri);
        }
    }
}
