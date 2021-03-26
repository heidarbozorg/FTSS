using FTSS.API.Extensions;
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
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        private string _apiAddress;

        #region Private methods
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
        /// Check user authorization for the new request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool IsAccessToCurrentRequest(AuthorizationFilterContext context)
        {
            var data = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs()
            {
                Token = context.HttpContext.User.GetToken(),
                APIAddress = _apiAddress
            };

            //Get default ORM
            var dbCTX = context.HttpContext.RequestServices.GetService(typeof(FTSS.Logic.Database.IDatabaseContext)) 
                            as FTSS.Logic.Database.IDatabaseContext;

            //Check user authorization
            var rst = Logic.Security.Common.IsUserAccessToAPI(dbCTX, data);
            return rst;
        }
        #endregion Private methods


        /// <summary>
        /// Check user authorization by checking JWT token placed at header
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Get the API address
            GetAPIAddress(context);

            //Check is current user access to the API
            if (!IsAccessToCurrentRequest(context))
            {
                //If user should not access this API
                context.Result = new UnauthorizedObjectResult("Access denied");
            }
        }
    }
}
