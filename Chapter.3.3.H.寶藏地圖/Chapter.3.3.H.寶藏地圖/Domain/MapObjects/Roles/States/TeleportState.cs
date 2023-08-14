namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

public class TeleportState : State
{
    public TeleportState(Role role) : base(role)
    {
    }

    public override string Name => "暖身狀態";
    protected override int Timeliness { get; set; } = 1;

    internal override void EndRoundAction()
    {
        if (Timeliness > 0)
        {
            Timeliness--;
            if (Timeliness is 0)
            {
                RandomMove();
                Role.SetState(GetStateAfterTimeliness());
            }
        }
    }

    private void RandomMove()
    {
        Console.WriteLine($"{Role.Symbol}要被隨機移動到空地拉");
        var emptyPositions = Role.Map.GetEmptyPositions();
        var random = new Random();
        var randomIndex = random.Next(0, emptyPositions.Count());
        var emptyPosition = emptyPositions.ElementAt(randomIndex);
        Role.Map.Move(Role, emptyPosition);
    }
}