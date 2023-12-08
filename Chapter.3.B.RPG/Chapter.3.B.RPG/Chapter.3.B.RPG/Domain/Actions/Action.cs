using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions;

public abstract class Action
{
    public abstract string Name { get; }
    public abstract int TargetCount { get; }
    public abstract int MpCost { get; }
    public Role Role { get; set; }

    public abstract void Execute(IEnumerable<Role> targets);
    public abstract IEnumerable<Role> GetCandidates();
}