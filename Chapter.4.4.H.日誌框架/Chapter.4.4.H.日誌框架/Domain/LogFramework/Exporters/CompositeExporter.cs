namespace Chapter._4._4.H.日誌框架.Domain.LogFramework.Exporters;

public class CompositeExporter : Exporter
{
    private IEnumerable<Exporter> Exporters { get; }

    public CompositeExporter(params Exporter[] exporters)
    {
        Exporters = exporters;
    }

    public override void Export(string message)
    {
        foreach (var exporter in Exporters)
        {
            exporter.Export(message);
        }
    }
}