using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.States;

public class PetrochemicalState : State
{
    public PetrochemicalState(Role role) : base(role)
    {
    }

    protected override int Timeliness { get; set; } = 3;
    protected override string Name => "石化";

    public override void ExecuteAction()
    {
        // 啥事都不能做
    }
}