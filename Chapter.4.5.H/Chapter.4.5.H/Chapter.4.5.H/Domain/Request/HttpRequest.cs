namespace Chapter._4._5.H.Domain.Request;

public class HttpRequest
{
    public HttpRequest(Url url)
    {
        Url = url;
        ActualHost = url.Host;
    }

    /// <summary>
    /// Only Support GET
    /// </summary>
    public string HttpMethod => "GET";

    public Url Url { get; }
    public IEnumerable<string> AvailableIps { get; set; } = Enumerable.Empty<string>();
    public string ActualHost { get; set; }

    public override string ToString() => $"{Url.Scheme}://{ActualHost}/{Url.Path}";
}