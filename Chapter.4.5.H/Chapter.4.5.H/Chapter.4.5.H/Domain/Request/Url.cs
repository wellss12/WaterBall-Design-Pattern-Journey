namespace Chapter._4._5.H.Domain.Request;

public class Url
{
    public Url(Scheme scheme, string host, string path)
    {
        Host = host;
        Path = path;
        Scheme = scheme;
    }

    public string Host { get; }
    public string Path { get; }
    public Scheme Scheme { get; }
}