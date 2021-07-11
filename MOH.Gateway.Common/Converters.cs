using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace MOH.Gateway.Common
{
    public static class Converters
    {
        public static string ConvertToJSON<T>(this T entity)
        {
            return JsonConvert.SerializeObject(entity);
        }
        public static HttpResponseMessage ReturnHttpResponseSuccess(this string content)
        {
            string result = content;
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result, Encoding.UTF8, "text/plain")
            };
            return resp;
        }
    }
}
