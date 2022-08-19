using System.Net;
using CSharpBlog.Exception.Exceptions;
using CSharpBlog.Exception.Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CSharpBlog.Exception.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (System.Exception ex)
        {
            _logger.LogError($"Something Went wrong while processing {httpContext.Request.Path}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    
    // Settings Error
    private Task HandleExceptionAsync(HttpContext context, System.Exception exception)
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        var errorDetails =  new ErrorDetails
        {
            ErrorType = "Failure",
            ErrorMessage = exception.Message
        };

        switch (exception)
        {
            // Not found exception
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorDetails = new ErrorDetails
                {
                    ErrorType = "NotFound",
                    ErrorMessage = notFoundException.Message
                };
                break;
            // Bad request exception
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                errorDetails = new ErrorDetails
                {
                    ErrorType = "BadRequest",
                    ErrorMessage = badRequestException.Message
                };
                break;
            default:
                break;
        }
        context.Response.StatusCode = (int)statusCode;
        var json = JsonConvert.SerializeObject(errorDetails);
        return context.Response.WriteAsync(json);

    }
}