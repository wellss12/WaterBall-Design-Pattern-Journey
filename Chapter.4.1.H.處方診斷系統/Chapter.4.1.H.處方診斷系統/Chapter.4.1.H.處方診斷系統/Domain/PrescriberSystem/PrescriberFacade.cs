using System.Collections.Concurrent;
using Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.PrescriberRule;
using System.Text.Json;
using Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.Subscribers;
using Chapter._4._1.H.處方診斷系統.Utility;

namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

public class PrescriberFacade
{
    private readonly Prescriber _prescriber;
    private readonly PatientDatabase _patientDatabase;

    public PrescriberFacade(string patientsFile, string potentialDiseaseFile)
    {
        _patientDatabase = new PatientDatabase(LoadPatientDataFrom(patientsFile));
        _prescriber = new Prescriber(LoadPotentialDiseaseDataFrom(potentialDiseaseFile));
        new Thread(Monitor).Start();
    }

    private readonly ConcurrentQueue<PrescriptionDemand?> _queue = new();

    private readonly Dictionary<string, PrescriptionRuleHandler> _ruleHandlerMap = new()
    {
        {"COVID-19", new COVID19Handler()},
        {"Attractive", new AttractiveHandler()},
        {"SleepApneaSyndrome", new SleepApneaSyndromeHandler()}
    };

    public void Prescribe(string id, string[] symptoms)
    {
        var patient = _patientDatabase.Find(id);
        if (patient is null)
        {
            throw new Exception("Not Found");
        }

        var prescriptionDemand = new PrescriptionDemand(patient, symptoms);
        _queue.Enqueue(prescriptionDemand);
        Console.WriteLine($"病人 {id} 已排入診斷對列，請稍後。");
    }

    private static IEnumerable<Patient> LoadPatientDataFrom(string jsonFileName)
    {
        var filePath = FileUtility.GetFilePath(jsonFileName);
        using var reader = new StreamReader(filePath);
        var json = reader.ReadToEnd();
        var patients = JsonSerializer.Deserialize<IEnumerable<Patient>>(json);

        return patients!;
    }

    private PrescriptionRuleHandler LoadPotentialDiseaseDataFrom(string textFileName)
    {
        var filePath = FileUtility.GetFilePath(textFileName);
        var reader = File.OpenText(filePath);

        var diseases = new List<string>();
        while (reader.ReadLine() is { } disease)
        {
            diseases.Add(disease);
        }

        return CreatePrescriptionRule(diseases);
    }


    private PrescriptionRuleHandler CreatePrescriptionRule(IEnumerable<string> diseases)
    {
        var handlers = diseases
            .Where(disease => _ruleHandlerMap.TryGetValue(disease, out _))
            .Select(disease => _ruleHandlerMap[disease])
            .ToList();

        if (handlers.Any())
        {
            var targetRuleHandler = handlers.First();
            handlers.Skip(1).Aggregate(targetRuleHandler, (current, next) =>
            {
                current.SetNext(next);
                return next;
            });

            return targetRuleHandler;
        }

        return null!;
    }

    private void Monitor()
    {
        while (true)
        {
            if (_queue.Any())
            {
                _queue.TryDequeue(out var demand);
                var prescription = _prescriber.Prescribe(demand);

                var newCase = new Case(demand.Symptoms, DateTime.Now, prescription);
                demand.Patient.AddCase(newCase);
                _patientDatabase.Update(demand.Patient);

                ChooseFileFormat();
                _prescriber.OnPrescribed(newCase);
            }
            else
            {
                Thread.Sleep(1000);
            }
        }
    }

    private void ChooseFileFormat()
    {
        Console.WriteLine("要儲存的檔案格式為？(1) Json (2) CSV (3) Json & CSV");
        int answer;
        while (int.TryParse(Console.ReadLine(), out answer) is false ||
               Enum.IsDefined(typeof(FileFormat), answer) is false)
        {
            Console.WriteLine("請輸入 1 or 2 or 3");
        }

        ConfigureSubscriber((FileFormat) answer);
    }

    private void ConfigureSubscriber(FileFormat answer)
    {
        switch (answer)
        {
            case FileFormat.Json:
                _prescriber.Subscribe(new PrescriptionJSONFile());
                break;
            case FileFormat.CSV:
                _prescriber.Subscribe(new PrescriptionCSVFile());
                break;
            case FileFormat.JsonAndCSV:
                _prescriber.Subscribe(new PrescriptionJSONFile());
                _prescriber.Subscribe(new PrescriptionCSVFile());
                break;
        }
    }
}