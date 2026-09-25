using System.Text.Json;
using BrewOS.Api.Contracts;
using BrewOS.Api.Middleware;
using BrewOS.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;

namespace BrewOS.Tests.Api;

public class ExceptionHandlingMiddlewareTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [Fact]
    public async Task InvokeAsync_WhenUnhandledException_ReturnsInternalErrorResponse()
    {
        // Arrange
        var before = DateTime.UtcNow;

        // Act
        var result = await InvokeAsync(new InvalidOperationException("boom"));

        // Assert
        Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        Assert.Equal("application/json", result.ContentType);
        Assert.Equal("INTERNAL_ERROR", result.Body.Code);
        Assert.Equal("Unexpected server error occurred.", result.Body.Message);
        Assert.True(result.Body.Timestamp >= before.AddSeconds(-1));
        Assert.True(result.Body.Timestamp <= DateTime.UtcNow.AddSeconds(1));
    }

    [Fact]
    public async Task InvokeAsync_WhenKeyNotFoundException_ReturnsNotFoundMapping()
    {
        // Arrange
        const string message = "Coffee with id 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa' was not found in the catalog.";

        // Act
        var result = await InvokeAsync(new KeyNotFoundException(message));

        // Assert
        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        Assert.Equal("application/json", result.ContentType);
        Assert.Equal("NOT_FOUND", result.Body.Code);
        Assert.Equal(message, result.Body.Message);
        Assert.NotEqual(default, result.Body.Timestamp);
    }

    [Fact]
    public async Task InvokeAsync_WhenInsufficientBalanceException_ReturnsBadRequestMapping()
    {
        // Arrange
        var exception = new InsufficientBalanceException(balanceInCents: 100, requiredAmountInCents: 300);

        // Act
        var result = await InvokeAsync(exception);

        // Assert
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        Assert.Equal("application/json", result.ContentType);
        Assert.Equal("INSUFFICIENT_BALANCE", result.Body.Code);
        Assert.Equal(exception.Message, result.Body.Message);
        Assert.NotEqual(default, result.Body.Timestamp);
    }

    [Fact]
    public async Task InvokeAsync_WhenArgumentException_ReturnsInvalidArgumentMapping()
    {
        // Arrange
        const string message = "Unsupported coin. Accepted denominations: 5, 10, 20, 50, 100, 200 cents.";

        // Act
        var result = await InvokeAsync(new ArgumentException(message));

        // Assert
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        Assert.Equal("application/json", result.ContentType);
        Assert.Equal("INVALID_ARGUMENT", result.Body.Code);
        Assert.Equal(message, result.Body.Message);
        Assert.NotEqual(default, result.Body.Timestamp);
    }

    [Fact]
    public async Task InvokeAsync_WhenNoException_DoesNotAlterSuccessfulResponse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        RequestDelegate next = async httpContext =>
        {
            httpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            await Task.CompletedTask;
        };

        var middleware = new ExceptionHandlingMiddleware(
            next,
            NullLogger<ExceptionHandlingMiddleware>.Instance);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal(StatusCodes.Status204NoContent, context.Response.StatusCode);
        Assert.Equal(0, context.Response.Body.Length);
    }

    private static async Task<(int StatusCode, string? ContentType, ApiErrorResponse Body)> InvokeAsync(
        Exception exception)
    {
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        RequestDelegate next = _ => throw exception;

        var middleware = new ExceptionHandlingMiddleware(
            next,
            NullLogger<ExceptionHandlingMiddleware>.Instance);

        await middleware.InvokeAsync(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var json = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var body = JsonSerializer.Deserialize<ApiErrorResponse>(json, JsonOptions);

        Assert.NotNull(body);

        return (context.Response.StatusCode, context.Response.ContentType, body);
    }
}
