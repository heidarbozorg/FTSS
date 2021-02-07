using FTSS.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace FTSS.API.Filters
{
    public class APILogger : ActionFilterAttribute
    {
        #region private methods
        /// <summary>
        /// Get user Token (database Token) if user Authorized
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string GetUserToken(Microsoft.AspNetCore.Http.HttpContext ctx)
        {
            try
            {
                var user = ctx.User;
                if (user == null)
                    return null;

                var rst = user.GetToken();
                return rst;
            }
            catch (Exception e)
            {
                return null;
            }
        }        

        /// <summary>
        /// Get parameters sent to the API at body
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string getAPIParam(Microsoft.AspNetCore.Http.HttpContext ctx)
        {
            try
            {
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
        /// Get the API full address contains the domain name. Exp: https://API.Google.Com/FTSS/Users/Insert
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string getAPIFulllAddress(Microsoft.AspNetCore.Http.HttpContext ctx)
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
        private string getAPIResult(ActionExecutedContext ctx)
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
        /// Get parameters LogId
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        /// <remarks>
        /// We log two records per each API. The first one for comming, and the second one for result.
        /// For logging the result, we need the first LogId as the ParentId.
        /// </remarks>
        private int getAPILog_Parent(Microsoft.AspNetCore.Http.HttpContext ctx)
        {
            try
            {
                if (ctx.Request == null || ctx.Request.Headers == null)
                    return -1;

                Microsoft.Extensions.Primitives.StringValues id;
                if (!ctx.Request.Headers.TryGetValue("APILogId_Parent", out id))
                    return (-1);

                if (id.Count == 0)
                    return -1;

                int result = -1;
                if (!int.TryParse(id[0], out result))
                    return -1;

                return (result);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Get the API error message (if exist).
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string getAPIErrorMessage(ActionExecutedContext ctx)
        {
            try
            {
                if (ctx.Result == null)
                    return ("null");

                if (ctx.Result is OkObjectResult)
                    return "OK";

                if (ctx.Result is ObjectResult)
                {
                    var value = (ctx.Result as ObjectResult).Value;
                    if (value != null)
                        return (value.ToString());
                }

                return ("");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get the API result status code
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private int getAPIStatusCode(ActionExecutedContext ctx)
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
        /// Get API inputs params as the Database model for inserting in database
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private Models.Database.StoredProcedures.SP_APILog_Insert.Inputs GetInputsModel(Microsoft.AspNetCore.Http.HttpContext ctx)
        {
            //Creat the object for save at database
            var inputs = new Models.Database.StoredProcedures.SP_APILog_Insert.Inputs
            {
                APIAddress = getAPIFulllAddress(ctx),
                UserToken = GetUserToken(ctx),
                DataJSON = getAPIParam(ctx)
            };

            return inputs;
        }

        /// <summary>
        /// Get API results as the Database model for inserting in database
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private Models.Database.StoredProcedures.SP_APILog_Insert.Inputs GetOutputsModel(ActionExecutedContext context)
        {
            var ctx = context.HttpContext;

            //Creat the object for save at database
            var inputs = new Models.Database.StoredProcedures.SP_APILog_Insert.Inputs
            {
                APILogId_Parent = getAPILog_Parent(ctx),
                APIAddress = getAPIFulllAddress(ctx),
                UserToken = GetUserToken(ctx),
                DataJSON = getAPIResult(context),
                ErrorMessage = getAPIErrorMessage(context),
                StatusCode = getAPIStatusCode(context)
            };

            return inputs;
        }
        #endregion private methods

        private readonly Logic.Database.IDatabaseContext _dbCTX;

        public APILogger(Logic.Database.IDatabaseContext ctx)
        {
            _dbCTX = ctx;
        }

        /// <summary>
        /// Log API request params
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ctx = context.HttpContext;

            //Get input params as Database model
            var data = GetInputsModel(ctx);

            //Save inputs in database
            var dbResult = _dbCTX.SP_APILog_Insert(data);

            if (dbResult != null && dbResult.Data != null && dbResult.Data is Models.Database.StoredProcedures.SP_APILog_Insert.Outputs)
            {
                //If APILog was inserted successfully
                //Get the database APILogId
                var logId = (dbResult.Data as Models.Database.StoredProcedures.SP_APILog_Insert.Outputs).Id;

                //Save APILogId at request header for using it later in order to save API results.
                context.HttpContext.Request.Headers.Add("APILogId_Parent", logId.ToString());
            }

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Log API result
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Get input params as Database model
            var data = GetOutputsModel(context);

            //Save API results in database
            _dbCTX.SP_APILog_Insert(data);

            base.OnActionExecuted(context);
        }
    }
}
