namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role.States;

public class AcceleratedState : State
{
    public AcceleratedState(Role role) : base(role)
    {
    }

    public override string Name => "加速狀態";
    protected override int Timeliness { get; set; } = 3;

    protected override void RoundAction()
    {
        for (var count = 0; count < 2; count++)
        {
            base.RoundAction();
        }
    }

    protected override void OnDamaged(Role attacker)
    {
        Role.Hp -= attacker.AttackPower;
        if (Role.IsDead() is false)
        {
            Role.SetState(new NormalState(Role));
        }
        else
        {
            Role.Map.RemoveMapObjectAt(Role.Position);
        }
    }
}