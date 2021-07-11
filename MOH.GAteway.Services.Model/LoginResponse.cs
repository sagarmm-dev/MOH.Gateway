using System;
using System.Collections.Generic;
using System.Text;

namespace MOH.Gateway.Services.Model
{
    public class LoginResponse
    {
        public Int32 UserID { get; set; }
        public Guid AQUserID { get; set; }
        public string UserData { get; set; }
        public string UserName { get; set; }
        public string ProfilePic { get; set; }
        public string MobileNumber { get; set; }
        public string Twofactorauthentication { get; set; }
        public string EmailID { get; set; }
        public string FullNameArabic { get; set; }
        public string FullNameEnglish { get; set; }
        public string Rnumber { get; set; }
        public string Language { get; set; }
        public List<UserTypeEntity> UserType { get; set; }
        public string Token { get; set; }
        public int Status { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }

        public string AlternateContactNumber { get; set; }
        public string ContactAlternateEmail { get; set; }
    }
    public class UserTypeEntity
    {
        public int UserTypeID { get; set; }
        public string UserTypeCode { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}
