namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

internal class PatientDatabase
{
    private readonly Dictionary<string, Patient> _patients;

    public PatientDatabase(IEnumerable<Patient> patients)
    {
        _patients = patients.ToDictionary(patient => patient.Id, patient => patient);
    }

    public Patient? Find(string id) => _patients.TryGetValue(id, out var patient) ? patient : null;

    public void Update(Patient patient)
    {
        if (_patients.ContainsKey(patient.Id))
        {
            _patients[patient.Id] = patient;
        }
    }
}