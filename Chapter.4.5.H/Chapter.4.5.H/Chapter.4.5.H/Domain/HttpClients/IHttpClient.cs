using Chapter._4._5.H.Domain.Request;

namespace Chapter._4._5.H.Domain.HttpClients;

public interface IHttpClient
{
    public void SendRequest(HttpRequest request);
}