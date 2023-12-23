using Chapter._4._5.H.Domain.HttpClients;
using Chapter._4._5.H.Domain.Request;

namespace Chapter._4._5.H.Domain.Processors;

public abstract class HttpRequestProcessor : IHttpClient
{
    protected readonly IHttpClient Next;

    protected HttpRequestProcessor(IHttpClient next)
    {
        Next = next;
    }

    public abstract void SendRequest(HttpRequest request);
}