using Microsoft.AspNetCore.Http;
using FTSS.Logic.Log;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FTSS.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILog _logger;

        /// <summary>
        /// Inject dependecies
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionMiddleware(RequestDelegate next, ILog logger)
        {
            _logger = logger;
            _next = next;
        }

        /// <summary>
        /// Manage pipeline
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Error($"Unhandled exception: {ex}");
                await HandleExceptionAsync(httpContext);
            }
        }

        /// <summary>
        /// Manage exceptions
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync("Oops! Something went wrong.");
        }
    }
}
