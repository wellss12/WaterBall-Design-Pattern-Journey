namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem.Subscribers;

public interface IPrescriptionSubscriber
{
    void OnPrescribed(Case @case);
}