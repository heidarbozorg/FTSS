using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace FTSS.API.Filters
{
    public class APILog : ActionFilterAttribute
    {
        /*
        #region private methods
        /// <summary>
        /// دریافت عنوان وب سرویس
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string getAPITitle(Microsoft.AspNetCore.Http.HttpContext ctx)
        {
            try
            {
                var api = ctx.Request.Path.HasValue ? ctx.Request.Path.ToString() : "";
                if (string.IsNullOrEmpty(api))
                    return "";

                var keyVal = new Dictionary<string, string>()
                {
                    {"/api/ime/callupinsurance", "استحقاق سنجی"},
                    {"/api/sabteahval/estelam", "استعلام از سازمان ثبت احوال"},
                    {"/api/shahkar/estelam", "استعلام مالک شماره تلفن"},
                    {"/api/tarakonesh/insert", "درج تراکنش بانکی"},
                    {"/api/toka/savestatement", "توکا - ارسال اظهارنامه"},
                    {"/api/toka/saveinvoice", "توکا - ارسال قبض"},
                    {"/api/pos/get", "پوز - دریافت توکن"},
                    {"/api/haghbime/getsal", "حق بیمه های یک شخص به تفکیک سال"},
                };

                var rst = keyVal.GetValueOrDefault(api.ToLower());
                return (rst);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// دریافت پارامترهای ارسال شده به وب سرویس
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
        /// دریافت آدرس اینترنتی وب سرویس جاری
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
        /// دریافت نتیجه اجرای وب سرویس در قالب جی سان
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
        /// دریافت آی دی لاگ شماره 1 - قبل از اجرای وب سرویس
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private int getAPILog_Parent(Microsoft.AspNetCore.Http.HttpContext ctx)
        {
            try
            {
                if (ctx.Request == null || ctx.Request.Headers == null)
                    return -1;

                Microsoft.Extensions.Primitives.StringValues id;
                if (!ctx.Request.Headers.TryGetValue("APILogId_Parent", out id))
                    return (-1);

                return (int.Parse(id[0]));
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// دریافت متن خطا اعلام شده وب سرویس در زمان اجرای ناموفق
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private string getAPIErrorMessage(ActionExecutedContext ctx)
        {
            try
            {
                if (ctx.Result == null)
                    //در صورتی که اجرای وب سرویس هیچ نتیجه ای نداشت
                    return ("null");

                if (ctx.Result is OkObjectResult)
                    //در صورتی که با موفقیت اجرا شده بود
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
        #endregion private methods

        public Dataprovider.SalamatDBContext _salamaDBCtx;

        public APILog(Dataprovider.SalamatDBContext ctx)
        {
            _salamaDBCtx = ctx;
        }

        /// <summary>
        /// تمام پارامتر های ارسالی به ای پی آی را لاگ می کند
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ctx = context.HttpContext;

            //عنوان وب سرویس را بدست بیار
            var onvan = getAPITitle(ctx);

            //پارامترهای ارسال شده به وب سرویس را بدست بیار
            var param = getAPIParam(ctx);

            //کاربر جاری را بدست بیار
            //کاربر جاری از طریق فیلتر امنیتی که پیش از این اجرا شده، ایجاد شده است
            var user = Logic.Sec.jwtHelper.getCurrentUser(ctx);

            //آدرس اینترنتی کامل وب سرویس را بدست بیار
            var apiAddress = getAPIFulllAddress(ctx);

            //شی ثبت در دیتابیس را ایجاد کن
            var command = new Logic.DP.ws.APILog()
            {
                APIAddress = apiAddress,
                APILogId_Parent = 0,
                ChangeToken = user != null ? user.UserToken : "",
                DataJSON = param,
                MSG = "",
                Onvan = onvan
            };

            //سابقه فراخوانی این وب سرویس را در دیتابیس ثبت کن
            var dbResult = command.Insert(_salamaDBCtx, user);

            if (dbResult.isSuccess)
            {
                //در صورت موفقیت آمیز بودن عملیات درج لاگ در دیتابیس
                //آی دی لاگ درج شده در دیتابیس را بدست بیار
                var logId = (dbResult.Data as Entities.DB.SingleId).Id;

                //به منظور ثبت لاگ نتیجه وب سرویس، آی دی لاگ درج شده جاری را درون هدر ذخیره کن
                context.HttpContext.Request.Headers.Add("APILogId_Parent", logId.ToString());
            }

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// نتایج مربوط به سرویس ها را لاگ می کند
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var ctx = context.HttpContext;

            //عنوان وب سرویس را بدست بیار
            var onvan = getAPITitle(ctx);

            //کاربر جاری را بدست بیار
            //کاربر جاری از طریق فیلتر امنیتی که پیش از این اجرا شده، ایجاد شده است
            var user = Logic.Sec.jwtHelper.getCurrentUser(ctx);

            //آدرس اینترنتی کامل وب سرویس را بدست بیار
            var apiAddress = getAPIFulllAddress(ctx);

            //دریافت نتیجه اجرای وب سرویس
            var result = getAPIResult(context);

            //دریافت متن خطای وب سرویس
            var errorMessage = getAPIErrorMessage(context);

            var APILogId_Parent = getAPILog_Parent(ctx);

            var command = new Logic.DP.ws.APILog()
            {
                APIAddress = apiAddress,
                APILogId_Parent = APILogId_Parent,
                ChangeToken = user != null ? user.UserToken : "",
                DataJSON = result,
                MSG = errorMessage,
                Onvan = onvan
            };

            //نتیجه اجرای این وب سرویس را در دیتابیس ثبت کن
            var dbResult = command.Insert(_salamaDBCtx, user);

            base.OnActionExecuted(context);
        }
        */
    }
}
