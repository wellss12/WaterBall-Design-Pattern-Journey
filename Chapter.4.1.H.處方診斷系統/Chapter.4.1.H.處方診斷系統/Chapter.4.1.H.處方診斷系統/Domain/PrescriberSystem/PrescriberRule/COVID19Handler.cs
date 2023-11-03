namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.PrescriberRule;

public class COVID19Handler : PrescriptionRuleHandler
{
    protected override bool IsMatch(PrescriptionDemand prescriptionDemand)
    {
        var symptoms = prescriptionDemand.Symptoms.ToArray();
        var hasHeadache = symptoms.Contains("Headache");
        var hasCough = symptoms.Contains("Cough");
        return hasHeadache && hasCough;
    }

    protected override Prescription DoHandling()
    {
        const string name = "清冠一號";
        const string potentialDisease = "新冠肺炎（專業學名：COVID-19）";
        const string usage = "將相關藥材裝入茶包裡，使用500 mL 溫、熱水沖泡悶煮1~3 分鐘後即可飲用。";
        const string medicine = "清冠一號";
        
        return new Prescription(name, potentialDisease, usage, medicine);
    }
}