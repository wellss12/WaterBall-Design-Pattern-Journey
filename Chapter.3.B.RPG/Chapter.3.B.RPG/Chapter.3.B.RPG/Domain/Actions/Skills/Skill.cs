using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public abstract class Skill : Action
{
    private void PreAction(IEnumerable<Role> targets)
    {
        var targetNames = string.Join(", ", targets.Select(target => target.ToString()));

        Console.WriteLine(targets.Any()
            ? $"{Role} 對 {targetNames} 使用了 {Name}。"
            : $"{Role} 使用了 {Name}。");
    }

    public override void Execute(IEnumerable<Role> targets)
    {
        PreAction(targets);
        Action(targets);
        PostAction();
    }

    protected abstract void Action(IEnumerable<Role> targets);

    private void PostAction() => Role.Mp -= MpCost;
}