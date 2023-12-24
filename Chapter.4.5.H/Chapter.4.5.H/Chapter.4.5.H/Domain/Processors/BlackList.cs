using Chapter._4._5.H.Domain.HttpClients;
using Chapter._4._5.H.Domain.Request;

namespace Chapter._4._5.H.Domain.Processors;

public class BlackList : HttpRequestProcessor
{
    private readonly string[] _values;

    public BlackList(IHttpClient next, string[] values) : base(next)
    {
        _values = values;
    }

    public override void SendRequest(HttpRequest request)
    {
        Validate(request);
        Next.SendRequest(request);
    }

    private void Validate(HttpRequest request)
    {
        if (_values.Contains(request.ActualHost))
        {
            throw new NotSupportedException("The host is in the black list.");
        }
    }
}