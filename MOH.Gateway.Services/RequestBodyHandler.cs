using Microsoft.AspNetCore.Http;
using MOH.Gateway.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MOH.Gateway.Services
{
    public class RequestBodyHandler : IRequestBodyHandler
    {
        /// <summary>
        /// Reads incoming request data
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public async Task<StringContent> GetFormattedBody(HttpRequest httpRequest) => await Task.Run(() =>
        {
            var dicBody = httpRequest.HasFormContentType ? httpRequest.Form?.ToDictionary(x => x.Key, x => x.Value.ToString()) : new Dictionary<string, string>();
            var dicHeader = httpRequest.Headers?.ToDictionary(x => x.Key, x => x.Value.ToString());
            foreach (var item in dicBody)
            {
                if (!dicHeader.ContainsKey(item.Key))
                    dicHeader.Add(item.Key, item.Value);
            }
            var dicJson = JsonConvert.SerializeObject(dicHeader);
        
            return new StringContent(dicJson, Encoding.UTF8, "application/json");
            //return default(StringContent);

        });
    }
}
