namespace ApiGateway.Interfaces;

public interface IProxyService
{
    Task<HttpResponseMessage> ForwardAsync(HttpContext context, string targetUrl);
}