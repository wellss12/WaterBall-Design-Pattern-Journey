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
        RoundRobin(request);
        Next.SendRequest(request);
    }

    private void RoundRobin(HttpRequest request)
    {
        if (request.AvailableIps.Any())
        {
            var host = request.Url.Host;
            if (!RoundRobinIndexMap.TryAdd(host, 0))
            {
                RoundRobinIndexMap[host]++;
            }

            var index = RoundRobinIndexMap[host] % request.AvailableIps.Count();
            request.ActualHost = request.AvailableIps.ElementAt(index);
        }
    }
}