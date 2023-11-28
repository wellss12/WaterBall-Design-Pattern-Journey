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

    // public virtual bool CanExecuteAction() => true;
    public virtual void ExecuteAction() => Role.ExecuteAction();

    public virtual void EndRoundAction()
    {
        Timeliness--;
        if (Timeliness <= 0)
        {
            Role.State = new NormalState(Role);
        }
    }
}