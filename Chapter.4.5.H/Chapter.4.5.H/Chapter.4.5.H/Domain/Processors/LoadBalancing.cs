using Chapter._4._5.H.Domain.HttpClients;
using Chapter._4._5.H.Domain.Request;

namespace Chapter._4._5.H.Domain.Processors;

public class LoadBalancing : HttpRequestProcessor
{
    private Dictionary<string, int> RoundRobinIndexMap { get; } = new();

    public LoadBalancing(IHttpClient next) : base(next)
    {
    }

    public override void SendRequest(HttpRequest request)
    {
        if (request.AvailableIps.Any())
        {
            if (!RoundRobinIndexMap.TryAdd(request.Url.Host, 0))
            {
                RoundRobinIndexMap[request.Url.Host]++;
            }

            var index = RoundRobinIndexMap[request.Url.Host] % request.AvailableIps.Count();
            request.ActualHost = request.AvailableIps.ElementAt(index);
        }

        Next.SendRequest(request);
    }
}