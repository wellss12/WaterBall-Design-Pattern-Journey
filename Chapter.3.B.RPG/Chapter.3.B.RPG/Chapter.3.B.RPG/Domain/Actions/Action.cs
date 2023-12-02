using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions;

public abstract class Action
{
    public abstract string Name { get; }
    public virtual int MpCost => 0;
    public virtual int TargetCount => 0;
    public virtual int Str => 0;
    public Role Role { get; set; }

    public abstract void Execute(IEnumerable<Role> targets);
    public abstract IEnumerable<Role> GetCandidates();
}