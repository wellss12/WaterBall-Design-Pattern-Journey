namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

internal class PatientDatabase
{
    private Dictionary<string, Patient> Patients { get; } = new();

    public Patient? Find(string id) => Patients.TryGetValue(id, out var patient) ? patient : null;

    public void Update(Patient patient)
    {
        if (Patients.ContainsKey(patient.Id))
        {
            Patients[patient.Id] = patient;
        }
    }

    public void AddRange(IEnumerable<Patient>? patients)
    {
        if (patients is null)
        {
            return;
        }
        foreach (var patient in patients)
        {
            Patients.Add(patient.Id, patient);
        }
    }
}