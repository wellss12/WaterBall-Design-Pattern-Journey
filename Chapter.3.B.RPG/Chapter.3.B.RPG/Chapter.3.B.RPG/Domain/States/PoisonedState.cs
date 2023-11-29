using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.States;

public class PoisonedState : State
{
    public PoisonedState(Role role) : base(role)
    {
    }

    protected override int Timeliness { get; set; } = 3;

    protected override string Name => "中毒";

    public override void ExecuteAction()
    {
        Role.Hp -= 30;
        if (Role.IsAlive())
        {
            Role.ExecuteAction();
        }
        else
        {
            Console.WriteLine($"{Role} 死亡。");
        }
    }
}