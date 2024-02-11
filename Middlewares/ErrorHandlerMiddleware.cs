using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using base_dotnet.Common;
using base_dotnet.Common.Exceptions;

namespace base_dotnet.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var response = context.Response;
            response.ContentType = "application/json";

            var errorDetail = new ErrorDetails()
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                Message = exception.Message
            };
            switch (exception)
            {
                case NotFoundException e:
                    // Custom not found
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnAuthorizedException e:
                    // Custom not found
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case ForbiddenException e:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case BadRequestException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorDetail.Message = "Object validation error.";
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorDetail.Message = exception.Message;
                    break;
            }

            errorDetail.StatusCode = response.StatusCode;
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            await response.WriteAsync(JsonSerializer.Serialize(errorDetail, serializeOptions));
        }
    }
}