using System.Text.Json;
using Chapter._4._1.H.處方診斷系統.Utility;

namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.Subscribers;

public class PrescriptionJSONFile : IPrescriptionSubscriber
{
    public void OnPrescribed(Case @case)
    {
        var fileName = $"{@case.Prescription.Name}診斷結果.json";
        var filePath = FileUtility.GetFilePath(fileName);
        var options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        var patientsJson = JsonSerializer.Serialize(@case, options);
        
        File.WriteAllText(filePath, patientsJson);
        Console.WriteLine($"{fileName} 已產生");
    }
}