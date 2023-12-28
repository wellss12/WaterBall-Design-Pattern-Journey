namespace Chapter._5._1.H.計算模型.Domain.Proxies;

public class VirtualComputationModelProxy : IModel
{
    private readonly string _modelName;
    private IModels? _models;

    public VirtualComputationModelProxy(string modelName)
    {
        _modelName = modelName;
    }

    public double[] LinearTransform(double[] vector)
    {
        _models ??= ComputationModels.GetInstance();
        var model = _models.CreateModel(_modelName);
        return model.LinearTransform(vector);
    }
}