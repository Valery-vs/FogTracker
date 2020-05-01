namespace FogTracker.Web.Middleware
{
    using System;
    using System.Net;
    using System.Security.Authentication;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);               
            }
            catch (Exception ex)
            {                
                await this.HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            this.logger.LogError(exception, "Error during request:");            

            var statusCode = context.Response.StatusCode != (int)HttpStatusCode.OK ? context.Response.StatusCode : (int)HttpStatusCode.InternalServerError;
            if (exception is AuthenticationException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exception is OperationCanceledException)
            {
                statusCode = 499; // Client Closed Request
            }

            var result = JsonConvert.SerializeObject(new { errorMessage = exception.Message });

            try
            {
                if (context.Response.ContentType == null || !context.Response.ContentType.Contains("application/json"))
                {
                    context.Response.ContentType = "application/json";
                }

                context.Response.StatusCode = statusCode; // 500 if unexpected;
            }
            catch
            {
            }

            return context.Response.WriteAsync(result);
        }
    }
}