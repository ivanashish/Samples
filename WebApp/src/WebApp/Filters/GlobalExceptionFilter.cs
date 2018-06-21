using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApp.CustomExceptions;

namespace WebApp.Filters
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        private string responseMessage;
        private HttpStatusCode statusCode;
        private ILogger<GlobalExceptionFilter> logger;

        #region [ctor]
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            this.logger = logger;
        }
        #endregion

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            this.ParseException(context.Exception);

            this.logger.LogError(0, context.Exception, context.Exception.Message);

            var response = context.HttpContext.Response;
            response.StatusCode = (int)this.statusCode;
            response.ContentType = "application/json";
            await response.WriteAsync(this.responseMessage);
            context.ExceptionHandled = true;
        }

        private void ParseException(Exception ex)
        {
            this.statusCode = HttpStatusCode.InternalServerError;
            var errorCode = "Unhandled";
            var errorMessage = string.Empty;

            if (ex is UnauthorizedAccessException)
            {
                this.statusCode = HttpStatusCode.Unauthorized;
                errorMessage = "Unauthorized Access.";
            }
            else if (ex is BusinessException)
            {
                this.statusCode = HttpStatusCode.BadRequest;
                errorMessage = ex.Message;
                errorCode = "";
            }

            this.responseMessage = JsonConvert.SerializeObject(new { ErrorCode = errorCode, ErrorMessage = errorMessage });
        }
    }
}
