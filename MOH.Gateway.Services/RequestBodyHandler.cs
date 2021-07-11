using Microsoft.AspNetCore.Http;
using MOH.Gateway.Services.Interfaces;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MOH.Gateway.Services
{
    public class RequestBodyHandler: IRequestBodyHandler
    {
     /// <summary>
     /// Reads incoming request data
     /// </summary>
     /// <param name="httpRequest"></param>
     /// <returns></returns>
        public async Task<HttpContent> GetFormattedBody(HttpRequest httpRequest)
        {
            var bytes = default(byte[]);

            using (Stream receiveStream = httpRequest.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream))
                {
                    using (var memstream = new MemoryStream())
                    {
                        await readStream.BaseStream.CopyToAsync(memstream);
                        bytes = memstream.ToArray();
                    }
                }
            }
            return new ByteArrayContent(bytes);
        }
    }
}
