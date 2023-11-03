using System.Collections.Concurrent;
using Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.Observer;
using Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.PrescriberRule;
using System.Text.Json;
using Chapter._4._1.H.處方診斷系統.Utility;

namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

public class PrescriberFacade
{
    public PrescriberFacade()
    {
        new Thread(Monitor).Start();
    }

    private readonly ConcurrentQueue<PrescriptionDemand?> _queue = new();

    private readonly Dictionary<string, PrescriptionRuleHandler> _ruleHandlerMap = new()
    {
        {"COVID-19", new COVID19Handler()},
        {"Attractive", new AttractiveHandler()},
        {"SleepApneaSyndrome", new SleepApneaSyndromeHandler()}
    };

    private PatientDatabase PatientDatabase { get; } = new();

    private Prescriber Prescriber { get; } = new();

    public void LoadPatientDataFrom(string jsonFileName)
    {
        var filePath = FileUtility.GetFilePath(jsonFileName);
        using var reader = new StreamReader(filePath);
        var json = reader.ReadToEnd();
        var patients = JsonSerializer.Deserialize<IEnumerable<Patient>>(json);

        PatientDatabase.AddRange(patients);
    }

    public void Prescribe(string id, string[] symptoms)
    {
        var patient = PatientDatabase.Find(id);
        if (patient is null)
        {
            throw new Exception("Not Found");
        }

        var prescriptionDemand = new PrescriptionDemand(patient, symptoms);
        _queue.Enqueue(prescriptionDemand);
        Console.WriteLine($"病人 {id} 已排入診斷對列，請稍後。");
    }

    public void LoadPotentialDiseaseDataFrom(string textFileName)
    {
        var filePath = FileUtility.GetFilePath(textFileName);
        var reader = File.OpenText(filePath);

        var diseases = new List<string>();
        while (reader.ReadLine() is { } disease)
        {
            diseases.Add(disease);
        }

        ConfigurePrescriptionRule(diseases);
    }

    private void ConfigurePrescriptionRule(IEnumerable<string> diseases)
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

            Prescriber.SetPrescriptionRuleHandler(targetRuleHandler);
        }
    }

    private void Monitor()
    {
        while (true)
        {
            if (_queue.Any())
            {
                _queue.TryDequeue(out var demand);
                var prescription = Prescriber.Prescribe(demand);
                Thread.Sleep(3000);
                Console.WriteLine($"{demand.Patient.Id} 已診斷完");

                var newCase = new Case(demand.Symptoms, DateTime.Now, prescription);
                demand.Patient.AddCase(newCase);
                PatientDatabase.Update(demand.Patient);

                ChooseFileFormat();
                Prescriber.OnPrescribed(newCase);
            }
            else
            {
                Thread.Sleep(1000);
            }
        }
    }

    private void ChooseFileFormat()
    {
        Console.WriteLine("要儲存的檔案格式為？(1) Json (2) CSV");
        int answer;
        while (int.TryParse(Console.ReadLine(), out answer) is false ||
               Enum.IsDefined(typeof(FileFormat), answer) is false)
        {
            Console.WriteLine("請輸入 1 或 2");
        }

        ConfigureSubscriber((FileFormat)answer);
    }

    private void ConfigureSubscriber(FileFormat answer)
    {
        switch (answer)
        {
            case FileFormat.Json:
                Prescriber.Subscribe(new PrescriptionJSONFile());
                break;
            case FileFormat.CSV:
                Prescriber.Subscribe(new PrescriptionCSVFile());
                break;
        }
    }
}