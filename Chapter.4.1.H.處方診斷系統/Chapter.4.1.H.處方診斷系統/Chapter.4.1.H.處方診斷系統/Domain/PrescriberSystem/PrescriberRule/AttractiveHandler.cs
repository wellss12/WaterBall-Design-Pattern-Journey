namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.PrescriberRule;

public class AttractiveHandler : PrescriptionRuleHandler
{
    protected override bool IsMatch(PrescriptionDemand prescriptionDemand)
    {
        return prescriptionDemand.Patient is {Age: 18, Gender: Gender.Female}
               && prescriptionDemand.Symptoms.Contains("Sneeze");
    }

    protected override Prescription DoHandling()
    {
        const string name = "青春抑制劑";
        const string potentialDisease = "有人想你了 (專業學名：Attractive)";
        const string usage = "把假鬢角黏在臉的兩側，讓自己異性緣差一點，自然就不會有人想妳了。";
        const string medicine = "假鬢角、臭味";

        return new Prescription(name, potentialDisease, usage, medicine);
    }
}