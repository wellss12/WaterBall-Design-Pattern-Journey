namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.PrescriberRule;

public class SleepApneaSyndromeHandler : PrescriptionRuleHandler
{
    protected override bool IsMatch(PrescriptionDemand prescriptionDemand)
    {
        var patient = prescriptionDemand.Patient;
        var weight = patient.Weight;
        var height = patient.Height / 100;
        var bmi = weight / (height * height);
        
        return bmi > 26 && prescriptionDemand.Symptoms.Contains("Snore");
    }

    protected override Prescription DoHandling()
    {
        const string name = "打呼抑制劑";
        const string potentialDisease = "睡眠呼吸中止症（專業學名：SleepApneaSyndrome）";
        const string usage = "睡覺時，撕下兩塊膠帶，將兩塊膠帶交錯黏在關閉的嘴巴上，就不會打呼了。";
        const string medicine = "一捲膠帶";

        return new Prescription(name, potentialDisease, usage, medicine);
    }
}