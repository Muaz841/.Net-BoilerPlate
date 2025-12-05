using BoilerPlate.Application.Exceptions;

namespace BoilerPlate.Api.GlobalExceptionHandlerMiddleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        string? code = null;
        string errorMessage = "An unexpected error occurred";

        switch (ex)
        {
            case CustomException ce:
                context.Response.StatusCode = ce is EntityNotFoundException ? 404 : 400;
                code = ce.Code;
                errorMessage = ce.Message;
                break;
            case UnauthorizedAccessException:
                context.Response.StatusCode = 401;
                break;
            default:
                context.Response.StatusCode = 500;
                break;
        }

        var response = new { code, error = errorMessage };
        return context.Response.WriteAsJsonAsync(response);
    }
}
