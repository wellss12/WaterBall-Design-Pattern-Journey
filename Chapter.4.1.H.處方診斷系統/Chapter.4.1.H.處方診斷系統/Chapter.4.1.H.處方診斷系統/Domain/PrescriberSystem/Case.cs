using CsvHelper.Configuration;

namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

public class Case
{
    public Case(IEnumerable<string> symptoms, DateTime createTime, Prescription prescription)
    {
        Symptoms = symptoms;
        CreateTime = createTime;
        Prescription = prescription;
    }

    public IEnumerable<string> Symptoms { get; }
    public DateTime CreateTime { get; }
    public Prescription Prescription { get; }
}