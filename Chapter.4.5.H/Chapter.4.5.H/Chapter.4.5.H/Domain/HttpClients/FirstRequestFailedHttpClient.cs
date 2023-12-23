using Chapter._4._5.H.Domain.Request;

namespace Chapter._4._5.H.Domain.HttpClients;

public class FirstRequestFailedHttpClient : IHttpClient
{
    private int _count;

    public void SendRequest(HttpRequest request)
    {
        if ( _count == 0)
        {
            _count++;
            throw new Exception("First request failed.");
        }

        Console.WriteLine($"[SUCCESS] {request}");
    }
}