using ApiGateway.Interfaces;

namespace ApiGateway.Services;

public class ProxyService : IProxyService
{
    private readonly IHttpClientFactory _factory;

    public ProxyService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<HttpResponseMessage> ForwardAsync(HttpContext context, string targetUrl)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(new HttpMethod(context.Request.Method), targetUrl);

        if (context.Request.ContentLength > 0)
        {
            request.Content = new StreamContent(context.Request.Body);
        }

        return await client.SendAsync(request);
    }
}