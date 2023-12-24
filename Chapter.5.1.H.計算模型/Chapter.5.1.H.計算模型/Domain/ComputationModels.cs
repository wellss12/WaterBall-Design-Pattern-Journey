namespace Chapter._5._1.H.計算模型.Domain;

public class ComputationModels : IModels
{
    private readonly Dictionary<string, IModel> _modelMap = new();
    private static readonly object LockObject = new();
    private static readonly ComputationModels Instance = new();
    
    private ComputationModels() => Console.WriteLine($"{nameof(ComputationModels)} created");

    public static IModels GetInstance() => Instance;

    public IModel CreateModel(string modelName)
    {
        lock (LockObject)
        {
            if (_modelMap.TryGetValue(modelName, out var value))
            {
                return value;
            }

            var realModel = new ComputationModel(ParseMatrix(modelName));
            _modelMap.Add(modelName, realModel);
            return realModel;
        }
    }

    private static double[,] ParseMatrix(string modelName)
    {
        var allLines = File.ReadAllLines($"Matrixs/{modelName}");
        var rowCount = allLines.Length;
        var colCount = allLines[0].Split(' ').Length;
        var matrix = new double[rowCount, colCount];

        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            var strings = allLines[rowIndex].Split(' ');
            for (var colIndex = 0; colIndex < colCount; colIndex++)
            {
                matrix[rowIndex, colIndex] = Convert.ToDouble(strings[colIndex]);
            }
        }

        return matrix;
    }
}