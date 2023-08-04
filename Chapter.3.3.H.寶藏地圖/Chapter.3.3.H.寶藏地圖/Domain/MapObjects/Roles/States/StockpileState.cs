namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

public class StockpileState : State
{
    public StockpileState(Role role) : base(role)
    {
        throw new NotImplementedException();
    }

    public override string Name => "蓄力狀態";
    protected override int Timeliness { get; set; } = 2;
    protected override State GetStateAfterTimeliness()
    {
        return new EruptingState(Role);
    }
}