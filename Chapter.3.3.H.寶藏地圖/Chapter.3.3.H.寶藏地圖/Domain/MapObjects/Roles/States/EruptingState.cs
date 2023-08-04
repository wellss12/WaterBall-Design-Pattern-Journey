namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

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
        var mapObjects = Role.Map.MapObjects;
        for (var row = 0; row < mapObjects.GetLength(0); row++)
        {
            for (var column = 0; column < mapObjects.GetLength(1); column++)
            {
                var mapObject = mapObjects[row, column];
                if (mapObject is Role attackedRole && attackedRole != Role)
                {
                    attackedRole.OnDamaged(50);
                }
            }
        }

        Console.WriteLine($"位於 {Role.Position} 的{Role.Symbol}是爆發狀態，全場都被攻擊了");
    }

    /// <summary>
    /// 三回合過後取得瞬身狀態
    /// </summary>
    /// <returns></returns>
    protected override State GetStateAfterTimeliness()
    {
        return new TeleportState(Role);
    }
}