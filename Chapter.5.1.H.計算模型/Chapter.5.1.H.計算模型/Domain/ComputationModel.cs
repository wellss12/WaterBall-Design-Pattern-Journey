namespace Chapter._5._1.H.計算模型.Domain;

public class ComputationModel : IModel
{
    private double[,] Matrix { get; }

    public ComputationModel(double[,] matrix)
    {
        Console.WriteLine($"{nameof(ComputationModel)} created");
        Matrix = matrix;
    }

    public double[] LinearTransform(double[] vector)
    {
        var rowLength = Matrix.GetLength(0);
        var columnLength = Matrix.GetLength(1);

        var newVector = new double[vector.Length];
        for (var rowIndex = 0; rowIndex < rowLength; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < columnLength; columnIndex++)
            {
                newVector[rowIndex] += Matrix[columnIndex, rowIndex] * vector[columnIndex];
            }
        }

        return newVector;
    }
}