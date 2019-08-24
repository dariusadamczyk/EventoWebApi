using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Evento.Api.Framework
{
    public class ErrorHandlerMiddleware
    {
        private RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(context, ex);
            }
        }

        private static Task HandleErrorAsync(HttpContext contex, Exception ex)
        {
            var exceptionType = ex.GetType();
            var statusCode = HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case Exception e when exceptionType == typeof(ArgumentException):
                    statusCode = HttpStatusCode.BadRequest;
                    break;
            }

            var response = new { message = ex.Message };

            var payload = JsonConvert.SerializeObject(response);
            contex.Response.ContentType = "application/json";
            contex.Response.StatusCode = (int)statusCode;

            return contex.Response.WriteAsync(payload);

        }
    }
}
