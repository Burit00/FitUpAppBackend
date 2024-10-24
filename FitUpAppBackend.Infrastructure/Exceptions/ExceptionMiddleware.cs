using FitUpAppBackend.Shared.Abstractions.Exceptions;
using Humanizer;
using Microsoft.AspNetCore.Http;

namespace FitUpAppBackend.Infrastructure.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(e, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext httpContext)
    {
        var (statusCode, error) = exception switch
        {
            FitUpException fitUpException => (fitUpException.StatusCode, 
                    new FitUpMiddlewareException(exception.GetType().Name.Replace("Exception", "").Underscore(),
                        exception.Message
                        )),
                    _ => (StatusCodes.Status500InternalServerError, new FitUpMiddlewareException("server_error", "Internal Server Error"))
        };
        
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(error);
    }
    
    private record FitUpMiddlewareException(string Code, string Message);
}