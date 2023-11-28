namespace Chapter._3.B.RPG.Domain.States;

public class NormalState : State
{
    public NormalState(Role role) : base(role)
    {
    }

    protected override int Timeliness { get; set; }
    protected override string Name => "正常";
}