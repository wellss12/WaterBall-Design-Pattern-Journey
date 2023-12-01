using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.States;

public class CheerupState : State
{
    public CheerupState(Role role) : base(role)
    {
    }

    protected override int Timeliness { get; set; } = 3;
    protected override string Name => "受到鼓舞";

    public override void Damage(Role target, int str)
    {
        base.Damage(target, str + 50);
    }
}