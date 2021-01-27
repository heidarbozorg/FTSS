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
        private string _apiAddress
        {
            get; set;
        }

        private string _jwtToken
        {
            get; set;
        }

        private Logic.Security.UserInfo _userModel
        {
            get; set;
        }

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
            var jwt = new Logic.Security.JWT(_jwtToken);
            if (jwt == null || !jwt.IsValid())
                return (false);

            _userModel = jwt.User;
            return true;
        }

        private string GetBody(AuthorizationFilterContext context)
        {
            try
            {
                var ctx = context.HttpContext;
                if (ctx.Request == null || ctx.Request.Body == null)
                    return null;

                var reader = new StreamReader(ctx.Request.Body);
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                var rst = reader.ReadToEnd();

                return (rst);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Check user access to this new request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool IsAccessToCurrentRequest(AuthorizationFilterContext context)
        {
            if (_userModel.AccessMenu == null 
                || _userModel.AccessMenu.Count == 0 ||
                _userModel.AccessMenu.FirstOrDefault(a => a.MenuAddress.ToLower().Equals(_apiAddress.ToLower())) == null)
                return (false);

            return true;
        }

        /// <summary>
        /// Check user authorization by checking JWT token placed at header
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
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

            //Save database token for APIs body
            //When we work with database, this token will be used
            context.HttpContext.Request.Headers.Add("Token", _userModel.User.Token);
        }
    }
}
