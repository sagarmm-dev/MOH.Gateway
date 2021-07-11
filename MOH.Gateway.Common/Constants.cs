using System;
using System.Collections.Generic;
using System.Text;

namespace MOH.Gateway.Common
{
   public static class Constants
    {
        #region Request Context Validation
        public const string GATEWAY_INVALID_HOST_URI = "Invalid host uri formed,please ensure the input parameters";
        public const string GATEWAY_MULTIPLE_ROUTE_FOUND = "Multiple route entries found!";
        public const string GATEWAY_ROUTE_NOTFOUND = "RouteEntry not found!";
        #endregion
    }
}
