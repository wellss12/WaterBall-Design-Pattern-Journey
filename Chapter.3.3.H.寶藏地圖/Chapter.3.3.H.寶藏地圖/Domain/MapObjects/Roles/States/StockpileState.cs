namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

public class StockpileState : State
{
    public StockpileState(Role role) : base(role)
    {
    }

    public override string Name => "蓄力狀態";
    protected override int Timeliness { get; set; } = 2;
    protected override State GetStateAfterTimeliness()
    {
        return new EruptingState(Role);
    }

    internal override void OnDamaged(int damage)
    {
        Role.Hp -= damage;
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