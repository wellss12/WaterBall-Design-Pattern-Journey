namespace Chapter._5._1.H.計算模型.Domain.Proxies;

public class VirtualComputationModelsProxy : IModels
{
    public IModel CreateModel(string modelName) => new VirtualComputationModelProxy(modelName);
}