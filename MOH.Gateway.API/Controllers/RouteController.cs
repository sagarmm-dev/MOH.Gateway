using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOH.Gateway.API.Ext;
using MOH.Gateway.API.Models;
using MOH.Gateway.Common;
using MOH.Gateway.Services.Interfaces;
using MOH.Gateway.Services.Model;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MOH.Gateway.API.Controllers
{
    [ApiController]
    public class RouteController : GateWayControllerBase
    {
        private IRoutingService _routingservice;
        private readonly IOptions<NoAuth> _noAuthConfig;

        public RouteController(IRoutingService routingservice, IOptions<NoAuth> noAuthConfig)
        {
            _routingservice = routingservice;
            _noAuthConfig = noAuthConfig;
        }

        /// <summary>
        /// Routes the incoming request to actual host
        /// </summary>d
        /// <returns>Returns the response data received from actual host</returns>
        [HttpPost, HttpGet, HttpDelete, HttpPut]
        [Route("{*url}")]
        public async Task<IActionResult> Route()
        {
            try
            {
                //Check for authorization
                if (!_noAuthConfig.Value.ApiEndPoints.ToLower().Contains(HttpContext.Request.Path.Value.TrimStart('/').ToLower()))
                {
                    var userToken = ValidateRequest();
                    if (userToken == null)
                        return new HttpResponseMessageResult(ReturnHttpResponseUnauthorizedResult());
                    HttpContext.Request.Headers.Add("userData1", EncryptDecrypt.Decrypt(userToken.Data).ToString());
                    HttpContext.Request.Headers.Add("userID1", EncryptDecrypt.Decrypt(userToken.Id).ToString());
                }

                //Api service call
                var response = ReturnWithHeader(await _routingservice.RouteAsync(HttpContext));

                //Check for Token generation on need basis
                if (_noAuthConfig.Value.ApiEndPontsWithToken.ToLower().Contains(HttpContext.Request.Path.Value.TrimStart('/').ToLower()))
                {
                    string contents = response.Content.ReadAsStringAsync().Result;
                    ServiceResult responseobject = JsonConvert.DeserializeObject<ServiceResult>(contents);
                    if (responseobject.code == 200)
                    {
                        LoginResponse responseLoginData = JsonConvert.DeserializeObject<LoginResponse>(responseobject.Data.ToString());
                        return new HttpResponseMessageResult(CreateToken(responseLoginData));
                    }
                }
                return new HttpResponseMessageResult(response);

            }
            catch (UnauthorizedAccessException)
            {
                return new HttpResponseMessageResult(ReturnHttpResponseUnauthorizedResult());
            }
            catch (TimeoutException)
            {
                return new HttpResponseMessageResult(ReturnHttpResponseTimeOutResult());
            }
            catch (Exception)
            {
                return new HttpResponseMessageResult(ReturnHttpResponseBadRequest());
            }
        }

    }
}
