using Microsoft.AspNetCore.Http;

namespace ApiGateway.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower() ?? string.Empty;

        if (path == "/" ||
            path.StartsWith("/health") ||
            path.StartsWith("/swagger") ||
            path.StartsWith("/api/v1/auth"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}