namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

public class HealingState : State
{
    public HealingState(Role role) : base(role)
    {
    }

    public override string Name => "恢復狀態";
    protected override int Timeliness { get; set; } = 5;

    protected internal override void PreRoundAction()
    {
        Role.Hp += 30;

        if (Role.IsFullHp())
        {
            Role.SetState(new NormalState(Role));
        }
    }
}