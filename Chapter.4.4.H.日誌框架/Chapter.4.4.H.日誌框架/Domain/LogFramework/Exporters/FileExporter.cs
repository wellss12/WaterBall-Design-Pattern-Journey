namespace Chapter._4._4.H.日誌框架.Domain.LogFramework.Exporters;

public class FileExporter : Exporter
{
    private readonly string _fileName;

    public FileExporter(string fileName)
    {
        _fileName = fileName;
    }

    public override void Export(string message)
    {
        using var file = File.Exists(_fileName)
            ? File.Open(_fileName, FileMode.Append)
            : File.Open(_fileName, FileMode.CreateNew);

        using var stream = new StreamWriter(file);
        stream.WriteLine(message);
    }
}