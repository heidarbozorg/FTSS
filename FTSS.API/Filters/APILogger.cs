using FTSS.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.API.Filters
{
    public class APILogger : ActionFilterAttribute
    {
        #region private methods
        ActionExecutingContext _executingContext;
        Logic.Log.IAPILogger _apiLogger;

        /// <summary>
        /// Get user Token (database Token) if user Authorized
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string GetUserToken(Microsoft.AspNetCore.Http.HttpContext ctx)
        {
            try
            {
                if (ctx == null || ctx.User == null)
                    return null;

                var user = ctx.User;
                var rst = user.GetToken();
                return rst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get parameters sent to the API at body
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string GetAPIParams(ActionExecutingContext context)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (var arg in context.ActionArguments)
                    sb.Append(arg.Key.ToString() + ": " + JsonConvert.SerializeObject(arg.Value) + "\n");

                return (sb.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get the API full address contains the domain name. Exp: https://API.Google.Com/FTSS/Users/Insert
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string GetAPIFulllAddress(Microsoft.AspNetCore.Http.HttpContext ctx)
        {
            try
            {
                var uriBuilder = new UriBuilder
                {
                    Scheme = ctx.Request.Scheme,
                    Host = ctx.Request.Host.Host,
                    Port = ctx.Request.Host.Port.GetValueOrDefault(80),
                    Path = ctx.Request.Path.ToString(),
                    Query = ctx.Request.QueryString.ToString()
                };

                var rst = uriBuilder.Uri.ToString();
                return (rst);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get the API result as json
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string GetAPIResult(ActionExecutedContext ctx)
        {
            try
            {
                if (ctx.Result == null)
                    return ("null");

                var rst = JsonConvert.SerializeObject(ctx.Result);
                return (rst);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get the API error message (if exist).
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string GetAPIErrorMessage(ActionExecutedContext ctx)
        {
            if (ctx == null || ctx.Result == null)
                return ("null");

            string rst = "";

            switch (ctx.Result)
            {
                case OkObjectResult ok:
                    rst = "OK";
                    break;

                case ObjectResult result:
                    var value = (ctx.Result as ObjectResult).Value;
                    if (value != null)
                        rst = value.ToString();
                    break;
            }

            return rst;
        }

        /// <summary>
        /// Get the API result status code
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private int GetAPIStatusCode(ActionExecutedContext ctx)
        {
            try
            {
                if (ctx.HttpContext == null || ctx.HttpContext.Response == null)
                    return 0;

                return ctx.HttpContext.Response.StatusCode;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Get API inputs/results as the model for save
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private Models.API.Log GetModel(ActionExecutedContext context)
        {
            var ctx = context.HttpContext;

            //Creat the object for save
            var inputs = new Models.API.Log
            {
                APIAddress = GetAPIFulllAddress(ctx),
                UserToken = GetUserToken(ctx),
                Params = GetAPIParams(_executingContext),
                Results = GetAPIResult(context),
                ErrorMessage = GetAPIErrorMessage(context),
                StatusCode = GetAPIStatusCode(context)
            };

            return inputs;
        }
        
        /// <summary>
        /// Fetch APILogger
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private Logic.Log.IAPILogger GetAPILogger(ActionExecutedContext context)
        {
            var rst = context.HttpContext.RequestServices.GetService(typeof(Logic.Log.IAPILogger))
                            as Logic.Log.IAPILogger;
            return rst;
        }

        /// <summary>
        /// Save API log
        /// </summary>
        /// <param name="context"></param>
        private async void SaveInDatabaseAsync(ActionExecutedContext context)
        {
            if (_apiLogger == null)
                return;
            await Task.Run(() =>
            {
                //Get input params as Database model
                var data = GetModel(context);

                //Save API results in database                
                _apiLogger.Save(data);
            });
        }
        #endregion private methods


        /// <summary>
        /// Log API request params
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Save context for fetch inputs
            _executingContext = context;
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Log API result
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Get database ORM from service pool
            _apiLogger = GetAPILogger(context);

            //Save log in database
            SaveInDatabaseAsync(context);

            base.OnActionExecuted(context);
        }
    }
}
