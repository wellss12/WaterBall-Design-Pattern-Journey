using Chapter._3._2.H.快捷鍵設置.MilitaryDevice;

namespace Chapter._3._2.H.快捷鍵設置.Commands;

public class TankMoveBackwardCommand : ICommand
{
    private readonly Tank _tank;

    public TankMoveBackwardCommand(Tank tank)
    {
        _tank = tank;
    }

    public string Name => $"Move{nameof(Tank)}Backward";


    public void Execute()
    {
        _tank.MoveBackward();
    }

    public void Undo()
    {
        _tank.MoveForward();
    }
}