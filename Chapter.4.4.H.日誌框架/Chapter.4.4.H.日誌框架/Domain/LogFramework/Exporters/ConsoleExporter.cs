namespace Chapter._4._4.H.日誌框架.Domain.LogFramework.Exporters;

public class ConsoleExporter : Exporter
{
    public override void Export(string message)
    {
        Console.WriteLine(message);
    }
}