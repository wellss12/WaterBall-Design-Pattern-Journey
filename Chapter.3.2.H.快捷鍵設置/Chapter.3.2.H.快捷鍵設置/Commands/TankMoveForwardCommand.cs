namespace Chapter._3._2.H.快捷鍵設置.Commands;

public class TankMoveForwardCommand : ICommand
{
    private readonly Tank _tank;

    public TankMoveForwardCommand(Tank tank)
    {
        _tank = tank;
    }

    public void Execute()
    {
        _tank.MoveForward();
    }

    public void Undo()
    {
        _tank.MoveBackward();
    }
}