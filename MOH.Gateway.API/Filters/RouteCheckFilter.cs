using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using MOH.Gateway.API.Models;
using MOH.Gateway.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MOH.Gateway.API.Filters
{
    public class RouteCheckFilter : IActionFilter
    {
        private readonly WrapperResultEntity _resultEntity = new WrapperResultEntity();

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var request = context.HttpContext.Request;
                var token = request.Cookies["sys_MOH_vn"] == null ? request.Headers["ApiKey"].ToString() : request.Cookies["sys_MOH_vn"];
                if (token == null)
                {
                    token = request.Headers["apikey"];
                }
                if (token == null || !ValidateToken(token))
                {
                    _resultEntity.Data = "Invalid Token";
                    _resultEntity.StatusCode = (int)ApplicationStatusCode.InvalidToken;
                    _resultEntity.Status = ApplicationStatus.Fail;

                    var cookie = new CookieHeaderValue("sys_MOH_vn", "Invalid Token");

                    var resultwithcookie = _resultEntity.ConvertToJSON().ReturnHttpResponseSuccess();

                    context.Result = new UnauthorizedResult();
                }
                await next();
            }
            catch (Exception ex)
            {
                //ApplicationLog.Writelog("Exception :" + ex.ToString() + ex.InnerException, "", "", ApplicationLog.LogType.Error, ex);
            }
        }
        private bool ValidateToken(string token)
        {
            try
            {
                if (token == null) return false;

                var userData = TokenManager.ValidateToken(HttpUtility.UrlDecode(token));

                if (userData != null && userData != "") return true; else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
