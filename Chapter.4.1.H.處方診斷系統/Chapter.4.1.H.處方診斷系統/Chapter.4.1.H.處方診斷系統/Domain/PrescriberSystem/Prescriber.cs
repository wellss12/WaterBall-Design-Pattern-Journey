using Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.PrescriberRule;
using Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.Subscribers;

namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

public class Prescriber
{
    private readonly PrescriptionRuleHandler _prescriptionRuleHandler;

    public Prescriber(PrescriptionRuleHandler prescriptionRuleHandler)
    {
        _prescriptionRuleHandler = prescriptionRuleHandler;
    }

    private readonly List<IPrescriptionSubscriber> _subscribers = new();

    public Prescription Prescribe(PrescriptionDemand demand)
    {
        Console.WriteLine($"{demand.Patient.Id} 開始診斷，診斷時間為 3 秒");
        Thread.Sleep(3000);
        var prescription = _prescriptionRuleHandler.Handle(demand);
        Console.WriteLine($"{demand.Patient.Id} 已診斷完");
        return prescription;
    }

    public void OnPrescribed(Case @case)
    {
        _subscribers.ForEach(subscriber => subscriber.OnPrescribed(@case));
        _subscribers.Clear();
    }

    public void Subscribe(IPrescriptionSubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }
}