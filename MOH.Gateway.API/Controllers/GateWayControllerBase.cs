using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOH.Gateway.API.Models;
using MOH.Gateway.Common;
using MOH.Gateway.Services.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;

namespace MOH.Gateway.API.Controllers
{
    public class GateWayControllerBase : ControllerBase
    {

        private readonly ServiceResult _resultEntity = new ServiceResult();
        protected HttpResponseMessage ReturnHttpResponseTimeOutResult()
        {
            _resultEntity.message = ApplicationStatus.Fail;
            _resultEntity.code = (int)ApplicationStatusCode.ExecutionTimeOut;
            return _resultEntity.ConvertToJSON().ReturnHttpResponseSuccess();
        }


        protected HttpResponseMessage ReturnHttpResponseUnauthorizedResult()
        {
            _resultEntity.message = ApplicationStatus.AuthorizationFail;
            _resultEntity.code = (int)ApplicationStatusCode.InvalidToken;
            return _resultEntity.ConvertToJSON().ReturnHttpResponseSuccess();
        }



        protected HttpResponseMessage ReturnHttpResponseSuccessResult(object resulObject)
        {
            _resultEntity.Data = resulObject;
            _resultEntity.code = (int)IntegrationStatusCode.Success;
            _resultEntity.message = ApplicationStatus.Success;
            return _resultEntity.ConvertToJSON().ReturnHttpResponseSuccess();
        }

        protected HttpResponseMessage ReturnHttpResponseBadRequest()
        {
            _resultEntity.message = ApplicationStatusCode.BadRequest.ToString();
            _resultEntity.code = (int)ApplicationStatusCode.BadRequest;
            return _resultEntity.ConvertToJSON().ReturnHttpResponseSuccess();
        }

        protected Token ValidateRequest()
        {
            var request = HttpContext.Request;
            var token = request.Cookies["sys_MOH_vn"] == null ? request.Headers["ApiKey"].ToString() : request.Cookies["sys_MOH_vn"];
            if (token == null)
                token = request.Headers["apikey"];
            if (token == null)
                return null;
            var validtoken = TokenManager.ValidateToken(HttpUtility.UrlDecode(token));
            return validtoken != null ? JsonConvert.DeserializeObject<Token>(validtoken) : null;
        }


        protected HttpResponseMessage ReturnWithHeader(HttpResponseMessage resp)
        {
            resp.Headers.Add("Access-Control-Expose-Headers", "sys_MOH_vn");  //Return old token
            resp.Headers.Add("sys_MOH_vn", GetRequestToken());  //Return old token
            return resp;
        }
        protected string GetRequestToken()
        {
            var request = HttpContext.Request;
            return request.Cookies["sys_MOH_vn"] == null ? request.Headers["ApiKey"].ToString() : request.Cookies["sys_MOH_vn"];
        }

        protected HttpResponseMessage CreateToken(LoginResponse responseobject)
        {
            Token entity;
            try
            {
                entity = new Token
                {
                    Data = EncryptDecrypt.Encrypt(responseobject.UserData),
                    Id = EncryptDecrypt.Encrypt(responseobject.UserID.ToString())
                };
                var token = TokenManager.GenerateToken(entity.ConvertToJSON());
                responseobject.Token = token;
                return ReturnHttpResponseSuccessResult(responseobject);
            }
            catch (Exception ex)
            {
                //ApplicationLog.Writelog("In UserActionController", "", "", ApplicationLog.LogType.Error, ex);
                return null;
            }
        }
    }
}
