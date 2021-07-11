using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOH.Gateway.API.Models
{
    public class ServiceResult
    {
        public string message { get; set; }

        public List<ErrorMessage> errors { get; set; }

        public int code { get; set; }

        public object Data { get; set; }

    }
    public static class ApplicationStatus
    {
        public static string Success = "success";
        public static string Fail = "fail";
        public static string Unsuccess = "unsuccess";
        public static string AuraportalServiceError = "Auraportal Service error";
        public static string InvalidUser = "Invalid User Credential";
        public static string UserAlreadyRegistered = "User Already Registered";
        public static string InvalidFormat = "Invalid Format";
        public static string PaymentDown = "Payment Down";
        public static string InvalidTableName = "Invalid Table Name";
        public static string NoRecordsFound = "No Records Found";
        public static string AuthorizationFail = "Authorization Failed";
        public static string InvalidInputParameters = "Invalid/Missing Input Parameters";
        public static string AlreadyExist = "The record is already exist";
        public static string NoRecordQRcodeFound = "There no official record found for this document, Please recheck";
        public static string ExpiredOtp = "OTP Expired";
        public static string InvalidOtp = "OTP Invalid";
    }
    public enum ApplicationStatusCode
    {
        BadRequest = 400,
        InSufficientData = 601,
        InvalidUserNamePassword = 602,
        InActiveUser = 603,
        InvalidToken = 604,
        Success = 605,
        Unsuccess = 606,
        LogoutSuccess = 607,
        AlreadyExist = 608,
        NoRecord = 609,
        InvalidFormat = 610,
        UserAlreadyRegistered = 611,
        InvalidOutstandingAmount = 1010,
        InvalidMinOutstandingAmount = 2020,
        LocationOutsideOman = 1011,
        ExecutionTimeOut = 1012,
        InvalidOTP = 1013,
        PaymentDown = 1014,
        NoRecordQRcode = 1015,
        ExpiredOTP = 1016
    }
    public enum IntegrationStatusCode
    {
        Success = 200,
        NotValidRequest = 400,
        NotFound = 404,
        Error = 204,
        Unsuccess = 606

    }
    public class ErrorMessage
    {
        public ErrorMessage()
        {

        }
        public ErrorMessage(string messge, string type, int error_code, string error_user_title, string error_user_message)
        {
            this.messge = messge;
            this.type = type;
            this.error_code = error_code;
            this.error_user_title = error_user_title;
            this.error_user_message = error_user_message;
        }
        public string messge { get; set; }
        public string type { get; set; }
        public int error_code { get; set; }
        public string error_user_title { get; set; }
        public string error_user_message { get; set; }

    }
    public class WrapperResultEntity
    {
        #region Public Properties
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public object Data { get; set; }
        #endregion
    }
}
