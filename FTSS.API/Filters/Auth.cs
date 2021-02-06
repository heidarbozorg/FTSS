using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Linq;

namespace FTSS.API.Filters
{
    /// <summary>
    /// Authorization user request by checking JWT token from header
    /// </summary>
    public class Auth : Attribute, IAuthorizationFilter
    {
        #region Private properties
        private string _apiAddress
        {
            get; set;
        }

        private string _jwtToken
        {
            get; set;
        }

        private Models.Database.StoredProcedures.SP_Login.Outputs _userModel
        {
            get; set;
        }
        #endregion Private properties

        #region Private methods
        /// <summary>
        /// دریافت مقدار یک متغیر از هدر درخواست ارسال شده
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetString(HttpContext ctx, string name)
        {
            try
            {
                var headers = ctx.Request.Headers[name];
                if (headers.Count == 0)
                    //در صورتی که نتوانستی اطلاعات را بدست بیاوری، با حروف کوچک امتحان کن
                    headers = ctx.Request.Headers[name.ToLower()];

                if (headers.Count == 0)
                    return null;

                var rst = headers.FirstOrDefault();
                return (rst);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Find requested api address
        /// </summary>
        /// <param name="context"></param>
        private void GetAPIAddress(AuthorizationFilterContext context)
        {
            try
            {
                _apiAddress = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : "";
            }
            catch (Exception)
            {
                _apiAddress = "";
            }
        }

        /// <summary>
        /// Catch JWT Token from header
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool GetJWTToken(AuthorizationFilterContext context)
        {
            //Get the JWT token from header
            var jwtToken = GetString(context.HttpContext, "Authorization");
            if (string.IsNullOrEmpty(jwtToken))
            {
                //If JWT token is empty
                return false;
            }

            jwtToken = jwtToken.Replace("Bearer ", "").Replace("bearer ", "");
            if (string.IsNullOrEmpty(jwtToken))
                return false;

            _jwtToken = jwtToken;
            return true;
        }

        /// <summary>
        /// Get UserInfo from JWT token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool GetUserModel(AuthorizationFilterContext context)
        {
            return true;
        }        

        /// <summary>
        /// Check user access to this new request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool IsAccessToCurrentRequest(AuthorizationFilterContext context)
        {
            return true;
        }
        #endregion Private methods


        /// <summary>
        /// Check user authorization by checking JWT token placed at header
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var jwt = context.HttpContext.RequestServices.GetService(Logic.Security.JWT);

            //Get the API address
            GetAPIAddress(context);

            //Get JWT token
            if (!GetJWTToken(context))
            {
                //If JWT token is empty
                context.Result = new UnauthorizedObjectResult("Invalid token");
                return;
            }

            //Get user info
            if (!GetUserModel(context))
            {
                //If JWT is not valid
                context.Result = new UnauthorizedObjectResult("Invalid token");
                return;
            }

            //Check is current user access to the API
            if (!IsAccessToCurrentRequest(context))
            {
                //If user should not access this API
                context.Result = new UnauthorizedObjectResult("Access denied");
                return;
            }
        }
    }
}
