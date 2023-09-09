namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

public class EruptingState : State
{
    public EruptingState(Role role) : base(role)
    {
    }

    public override string Name => "爆發狀態";
    protected override int Timeliness { get; set; } = 3;
    public override int AttackPower => 50;

    public override IEnumerable<Role> GetAttackableRoles()
    {
        Console.WriteLine($"位於 {Role.Position} 的 {Role.Symbol} 是{Name}，全場都要被攻擊了");

        foreach (var mapObject in Role.Map.MapCells)
        {
            if (mapObject is Role attackedRole)
            {
                yield return attackedRole;
            }
        }
    }

    protected override State GetStateAfterTimeliness() => new TeleportState(Role);
}