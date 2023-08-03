namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role.States;

public class NormalState : State
{
    public NormalState(Role role) : base(role)
    {
    }

    public override string Name => "正常狀態";
    protected override int Timeliness { get; set; }
}