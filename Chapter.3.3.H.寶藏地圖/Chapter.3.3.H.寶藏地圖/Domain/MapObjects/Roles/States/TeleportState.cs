namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

public class TeleportState : State
{
    public TeleportState(Role role) : base(role)
    {
    }

    public override string Name => "暖身狀態";
    protected override int Timeliness { get; set; } = 1;

    protected internal override void EndRoundAction()
    {
        if (Timeliness > 0)
        {
            Timeliness--;
            if (Timeliness is 0)
            {
                RandomMove();
                Role.SetState(new NormalState(Role));
            }
        }
    }

    private void RandomMove()
    {
        Console.WriteLine($"{Role.Symbol}要被隨機移動到空地拉");
        
        var emptyPositions = GetEmptyPositions();
        var random = new Random();
        var randomIndex = random.Next(1, emptyPositions.Count());
        var emptyPosition = emptyPositions.ElementAt(randomIndex);
        Role.Map.Move(Role, emptyPosition);
    }

    private IEnumerable<Position> GetEmptyPositions()
    {
        var mapCells = Role.Map.MapCells;
        for (var row = 0; row < mapCells.GetLength(0); row++)
        {
            for (var column = 0; column < mapCells.GetLength(1); column++)
            {
                if (mapCells[row, column] is null)
                {
                    yield return new Position(row, column);
                }
            }
        }
    }
}