using System.Net;
using System.Text.Json;
using BrewOS.Api.Contracts;
using BrewOS.Domain.Exceptions;

namespace BrewOS.Api.Middleware;

/// <summary>
/// Global exception handling middleware that catches unhandled exceptions,
/// logs them, and returns a consistent <see cref="ApiErrorResponse"/> JSON body.
/// </summary>
public sealed class ExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Creates the middleware.
    /// </summary>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the next middleware and translates exceptions into HTTP JSON responses.
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, error) = MapException(exception);

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            _logger.LogError(
                exception,
                "Unhandled exception for {Method} {Path}",
                context.Request.Method,
                context.Request.Path);
        }
        else
        {
            _logger.LogWarning(
                exception,
                "Handled exception for {Method} {Path}: {Message}",
                context.Request.Method,
                context.Request.Path,
                exception.Message);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(error, JsonOptions));
    }

    private static (HttpStatusCode StatusCode, ApiErrorResponse Error) MapException(Exception exception)
    {
        return exception switch
        {
            KeyNotFoundException => (
                HttpStatusCode.NotFound,
                new ApiErrorResponse
                {
                    Code = "NOT_FOUND",
                    Message = exception.Message
                }),
            InsufficientBalanceException => (
                HttpStatusCode.BadRequest,
                new ApiErrorResponse
                {
                    Code = "INSUFFICIENT_BALANCE",
                    Message = exception.Message
                }),
            ArgumentException => (
                HttpStatusCode.BadRequest,
                new ApiErrorResponse
                {
                    Code = "INVALID_ARGUMENT",
                    Message = exception.Message
                }),
            _ => (
                HttpStatusCode.InternalServerError,
                new ApiErrorResponse
                {
                    Code = "INTERNAL_ERROR",
                    Message = "Unexpected server error occurred."
                })
        };
    }
}
