using Chapter._4._5.H.Domain.HttpClients;
using Chapter._4._5.H.Domain.Processors;
using Chapter._4._5.H.Domain.Request;

var waterRequest = new HttpRequest(new Url(Scheme.https, "waterballsa.tw", "world"));
var googleRequest = new HttpRequest(new Url(Scheme.https, "google.com", "world"));
var hostIpMap = new Dictionary<string, IEnumerable<string>>
{
    {"waterballsa.tw", new[] {"35.0.0.1", "35.0.0.2", "35.0.0.3"}},
    {"google.com", new[] {"36.0.0.1", "36.0.0.2", "36.0.0.3"}}
};

var blackList = new[] {"waterballsa.tw"};
var client1 = new ServiceDiscovery(new LoadBalancing(new BlackList(new FakeHttpClient(), blackList)), hostIpMap);
client1.SendRequest(waterRequest);
client1.SendRequest(waterRequest);
client1.SendRequest(waterRequest);
client1.SendRequest(waterRequest);

var client2 = new ServiceDiscovery(new LoadBalancing(new BlackList(new FakeHttpClient(), blackList)), hostIpMap);
client2.SendRequest(googleRequest);
client2.SendRequest(googleRequest);
client2.SendRequest(googleRequest);
client2.SendRequest(googleRequest);

// new BlackList(
//     new LoadBalancing(
//         new ServiceDiscovery(
//             new FakeHttpClient(), hostIpMap)), blackList).SendRequest(request);

// var client = new ServiceDiscovery(new FirstRequestFailedHttpClient(), hostIpMap);
// client.SendRequest(request);
// client.SendRequest(request);
// Thread.Sleep(TimeSpan.FromSeconds(10));
// Console.WriteLine("10 seconds later...");
// client.SendRequest(request);
