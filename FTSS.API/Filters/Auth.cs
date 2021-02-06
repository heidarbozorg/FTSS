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
    public class Auth : Attribute, IAuthorizationFilter
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
        /// Check user access to this new request
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

            //Get access to database service
            var ctx = context.HttpContext.RequestServices.GetService(typeof(FTSS.Logic.Database.IDatabaseContext)) 
                            as FTSS.Logic.Database.IDatabaseContext;

            //Check at database
            var rst = Logic.Security.Common.IsUserAccessToAPI(ctx, data);
            return rst;
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
