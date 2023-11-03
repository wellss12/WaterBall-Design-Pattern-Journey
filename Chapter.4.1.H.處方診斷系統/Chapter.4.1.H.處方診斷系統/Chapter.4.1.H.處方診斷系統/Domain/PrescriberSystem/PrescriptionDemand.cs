namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

public class PrescriptionDemand
{
    public Patient Patient { get; }
    public string[] Symptoms { get; }

    public PrescriptionDemand(Patient patient, string[] symptoms)
    {
        Patient = patient;
        Symptoms = symptoms;
    }
}