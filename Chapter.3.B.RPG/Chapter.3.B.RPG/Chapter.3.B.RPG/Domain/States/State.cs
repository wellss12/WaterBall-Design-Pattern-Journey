using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.States;

public abstract class State
{
    protected readonly Role Role;

    protected State(Role role)
    {
        Role = role;
    }

    protected abstract int Timeliness { get; set; }
    protected abstract string Name { get; }
    public override string ToString() => $"State: {Name}";

    public virtual void ExecuteAction() => Role.ExecuteAction();

    public virtual void Damage(Role target, int str)
    {
        Console.WriteLine($"{Role} 對 {target} 造成 {str} 點傷害。");
        target.OnDamaged(str);
    }

    internal void EndRoundAction()
    {
        Timeliness--;
        if (Timeliness <= 0)
        {
            Role.State = new NormalState(Role);
        }
    }
}