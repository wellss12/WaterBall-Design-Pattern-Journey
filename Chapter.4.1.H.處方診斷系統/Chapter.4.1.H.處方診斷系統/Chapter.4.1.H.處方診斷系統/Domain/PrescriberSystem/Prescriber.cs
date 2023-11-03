using Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.Observer;
using Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.PrescriberRule;

namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

public class Prescriber
{
    private PrescriptionRuleHandler PrescriptionRuleHandler { get; set; }
    private List<IPrescriptionSubscriber> Subscribers { get; } = new();

    public Prescription Prescribe(PrescriptionDemand prescriptionDemand) =>
        PrescriptionRuleHandler.Handle(prescriptionDemand);

    public void OnPrescribed(Case @case)
    {
        Subscribers.ForEach(subscriber => subscriber.Update(@case));
        Subscribers.Clear();
    }

    public void Subscribe(IPrescriptionSubscriber subscriber)
    {
        Subscribers.Add(subscriber);
    }
    
    public void SetPrescriptionRuleHandler(PrescriptionRuleHandler ruleHandler)
    {
        PrescriptionRuleHandler = ruleHandler;
    }
}