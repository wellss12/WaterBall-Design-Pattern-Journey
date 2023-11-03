using System.Globalization;
using Chapter._4._1.H.處方診斷系統.Utility;
using CsvHelper;
using CsvHelper.Configuration;

namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.Observer;

public class PrescriptionCSVFile : IPrescriptionSubscriber
{
    public void Update(Case @case)
    {
        var fileName = $"{@case.Prescription.Name}診斷結果.csv";
        var filePath = FileUtility.GetFilePath(fileName);

        var writeConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        };
        using var csvWriter = new CsvWriter(new StreamWriter(filePath), writeConfiguration);
        csvWriter.Context.RegisterClassMap<CaseMap>();
        csvWriter.WriteHeader<Case>();
        csvWriter.NextRecord();
        csvWriter.WriteRecord(@case);

        Console.WriteLine($"{fileName} 已產生");
    }
}

public sealed class CaseMap : ClassMap<Case>
{
    public CaseMap()
    {
        Map(m => m.Symptoms).Name("Symptoms");
        Map(m => m.CreateTime).Name("CreateTime");
        Map(p => p.Prescription.Name).Name("PrescriptionName"); 
        Map(p => p.Prescription.PotentialDisease).Name("PotentialDisease"); 
        Map(p => p.Prescription.Medicines).Name("Medicines"); 
        Map(p => p.Prescription.Usage).Name("Usage"); 
    }
}