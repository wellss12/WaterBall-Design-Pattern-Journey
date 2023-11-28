using Chapter._3.B.RPG.Domain.States;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Poison : Skill
{
    public override string Name => "下毒";
    public override int TargetCount => 1;
    public override int MpCost => 80;
    public override int Str => 0;
    public override void Execute(IEnumerable<Role> targets)
    {
        var targetNames = targets.Select(target => target.ToString());
        var targetNameString = string.Join(", ", targetNames);
        Console.WriteLine($"{Role} 對 {targetNameString} 使用了 {Name}。");

        foreach (var target in targets)
        {
            target.State = new PoisonedState(target);
        }

        Role.Mp -= MpCost;
    }
}