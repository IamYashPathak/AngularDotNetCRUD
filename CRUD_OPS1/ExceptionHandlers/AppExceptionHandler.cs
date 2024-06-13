using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_OPS1.ExceptionHandlers
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorResponse()
            {
                statusCode = StatusCodes.Status500InternalServerError,
                message = "Something went wrong :("
            };

            if (exception is NotImplementedException)
            {
                response = new ErrorResponse()
                {
                    statusCode = StatusCodes.Status500InternalServerError,
                    message = exception.Message
                };
                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                return true;
            } else if (exception is AuthenticationFailureException) {
                response = new ErrorResponse()
                {
                    statusCode = StatusCodes.Status404NotFound,
                    message = exception.Message
                };
                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                return true;

            } 

            else if (exception is AuthenticationRequiredException) {
                response = new ErrorResponse()
                {
                    statusCode = StatusCodes.Status401Unauthorized,
                    message = exception.Message
                };
                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                return true;
            }
            else if (exception is UnauthorizedAccessException) {
                response = new ErrorResponse()
                {
                    statusCode = StatusCodes.Status403Forbidden,
                    message = exception.Message
                };
                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

                return true;
            } else if (exception is KeyNotFoundException) {
                response = new ErrorResponse()
                {
                    statusCode = StatusCodes.Status404NotFound,
                    message = exception.Message
                };
                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                return true;
            }

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return true;
        }
    }
}
