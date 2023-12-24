using Chapter._4._5.H.Domain.HttpClients;
using Chapter._4._5.H.Domain.Request;

namespace Chapter._4._5.H.Domain.Processors;

public class ServiceDiscovery : HttpRequestProcessor
{
    private readonly Dictionary<string, IEnumerable<string>> _hostIpMap;
    private readonly Dictionary<string, DateTime> _inValidHostIpMap = new();
    private readonly TimeSpan _inValidTime = TimeSpan.FromSeconds(10);

    public ServiceDiscovery(IHttpClient next, Dictionary<string, IEnumerable<string>> hostIpMap) : base(next)
    {
        _hostIpMap = hostIpMap;
    }

    public override void SendRequest(HttpRequest request)
    {
        CleanExpiredIps();
        Discover(request);

        try
        {
            Next.SendRequest(request);
        }
        catch (Exception e)
        {
            // 10 分鐘太久，改用 10 秒
            _inValidHostIpMap.Add(request.ActualHost, DateTime.Now.Add(_inValidTime));
            Console.WriteLine($"[ERROR] {e.Message}, Request: {request}");
        }
    }

    private void Discover(HttpRequest request)
    {
        if (_hostIpMap.TryGetValue(request.Url.Host, out var ips))
        {
            request.AvailableIps = ips.Except(_inValidHostIpMap.Keys);
            request.ActualHost = request.AvailableIps.First();
        }
        else
        {
            throw new NotSupportedException("The host is not in the hostIpMap.");
        }
    }

    private void CleanExpiredIps()
    {
        var expiredIps = _inValidHostIpMap
            .Where(kv => kv.Value <= DateTime.Now)
            .Select(kv => kv.Key)
            .ToList();

        foreach (var expiredIp in expiredIps)
        {
            _inValidHostIpMap.Remove(expiredIp);
        }
    }
}