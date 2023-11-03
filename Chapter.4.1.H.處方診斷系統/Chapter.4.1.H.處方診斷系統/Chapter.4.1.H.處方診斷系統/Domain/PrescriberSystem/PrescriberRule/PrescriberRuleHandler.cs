namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.PrescriberRule;

public abstract class PrescriptionRuleHandler
{
    private PrescriptionRuleHandler? Next { get; set; }

    public Prescription Handle(PrescriptionDemand prescriptionDemand)
    {
        if (IsMatch(prescriptionDemand))
        {
            return DoHandling();
        }
    
        return Next?.Handle(prescriptionDemand) ?? throw new NotSupportedException("No rule matched.");
    }
    
    protected abstract bool IsMatch(PrescriptionDemand prescriptionDemand);
    protected abstract Prescription DoHandling();

    public void SetNext(PrescriptionRuleHandler next)
    {
        Next = next;
    }
}