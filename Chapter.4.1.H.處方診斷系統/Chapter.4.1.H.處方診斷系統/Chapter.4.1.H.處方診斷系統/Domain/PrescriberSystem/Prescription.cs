namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

public class Prescription
{
    public Prescription(string name, string potentialDisease, string usage, params string[] medicines)
    {
        Validate(name, potentialDisease, usage, medicines);

        Name = name;
        PotentialDisease = potentialDisease;
        Medicines = medicines;
        Usage = usage;
    }

    private static void Validate(string name, string potentialDisease, string usage, string[] medicines)
    {
        if (name.Length is >= 4 and <= 30 is false)
        {
            throw new ArgumentException("Name length should be between 4 and 30.");
        }

        if (potentialDisease.Length is >= 8 and <= 100 is false)
        {
            throw new ArgumentException("Potential disease length should be between 8 and 100.");
        }

        if (medicines.All(t => t.Length is >= 4 and <= 30) is false)
        {
            throw new ArgumentException("Medicine length should be between 4 and 30.");
        }

        if (usage.Length <= 1000 is false)
        {
            throw new ArgumentException("Usage length should be less than 1000.");
        }
    }

    public string Name { get; }
    public string PotentialDisease { get; }
    public string[] Medicines { get; }
    public string Usage { get; }
}