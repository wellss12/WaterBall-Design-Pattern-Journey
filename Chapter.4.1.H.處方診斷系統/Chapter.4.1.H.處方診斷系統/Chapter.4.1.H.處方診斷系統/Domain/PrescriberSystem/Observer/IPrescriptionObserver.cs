namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.Observer;

public interface IPrescriptionSubscriber
{
    void Update(Case @case);
}