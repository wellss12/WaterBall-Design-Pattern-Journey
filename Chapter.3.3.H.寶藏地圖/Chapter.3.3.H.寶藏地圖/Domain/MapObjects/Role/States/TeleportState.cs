namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role.States;

public class TeleportState : State
{
    public TeleportState(Role role) : base(role)
    {
    }

    public override string Name => "暖身狀態";
    protected override int Timeliness { get; set; } = 1;

    protected override void EndRoundAction()
    {
        if (Timeliness > 0)
        {
            Timeliness--;
            if (Timeliness is 0)
            {
                Move();
                Role.SetState(new NormalState(Role));
            }
        }
    }

    protected override void Move()
    {
        var mapObjects = Role.Map.MapObjects;
        for (var row = 0; row < mapObjects.GetLength(0); row++)
        {
            for (var column = 0; column < mapObjects.GetLength(1); column++)
            {
                if (mapObjects[row, column] is null)
                {
                    Role.Map.RemoveMapObjectAt(Role.Position);
                    mapObjects[row, column] = Role;
                    Role.Position = new Position(row, column);
                    Console.WriteLine(
                        $"{Role.Symbol}被隨機移動到空地:[{row},{column}]");
                }
            }
        }
    }
}