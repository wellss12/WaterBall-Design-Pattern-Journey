namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role.States;

public class EruptingState : State
{
    public EruptingState(Role role) : base(role)
    {
    }

    public override string Name => "爆發狀態";
    protected override int Timeliness { get; set; } = 3;
    protected override void Attack()
    {
        // TODO: 全場攻擊
        base.Attack();
    }

    protected override State GetStateAfterTimeliness()
    {
        return new TeleportState(Role);
    }
}