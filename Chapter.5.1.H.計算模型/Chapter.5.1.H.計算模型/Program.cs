using Chapter._5._1.H.計算模型.Domain.Proxies;

var vector = GenerateVector();
const int clientCount = 1000;
var tasks = new Task[clientCount];
for (var i = 0; i < clientCount; i++)
{
    tasks[i] = Task.Run(() =>
    {
        var proxy = new VirtualComputationModelsProxy();
        var model = proxy.CreateModel("Scaling.mat");
        var newVector = model.LinearTransform(vector);
        Console.WriteLine($"Result: {newVector[0]}");
    });
}

await Task.WhenAll(tasks);
Console.WriteLine("Done");


# region Function

double[] GenerateVector()
{
    var result = new double[1000];
    for (var i = 0; i < result.Length; i++)
    {
        result[i] = 1.1;
    }

    return result;
}

# endregion