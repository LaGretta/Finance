using System.Net;
using System.Text.Json;
using FinanceAPI.Exceptions;

namespace FinanceAPI.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
          await HandleExceptionAsync(context, ex);
        }
    }
    public async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, ex.Message);

        var (statusCode, message) = ex switch
        {
            CustomExceptions.NotFoundExeption => (HttpStatusCode.NotFound, ex.Message),
            BadHttpRequestException => (HttpStatusCode.BadRequest, ex.Message),
            CustomExceptions.ConflictException => (HttpStatusCode.Conflict, ex.Message),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ex.Message),
            _ => (HttpStatusCode.InternalServerError, "Internal server error")
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            statusCode = (int)statusCode,
            message,
            timestamp = DateTimeOffset.UtcNow
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}