using Chapter._4._5.H.Domain.Request;

namespace Chapter._4._5.H.Domain.HttpClients;

public class FakeHttpClient : IHttpClient
{
    public void SendRequest(HttpRequest request) => Console.WriteLine($"[SUCCESS] {request}");
}