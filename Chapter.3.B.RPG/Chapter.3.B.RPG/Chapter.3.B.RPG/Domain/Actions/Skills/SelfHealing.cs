namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class SelfHealing : Skill
{
    public override string Name => "自我治療";
    public override int TargetCount => 1;
    public override int MpCost => 50;
    public override int Str => 0;

    public override void Execute(IEnumerable<Role> targets)
    {
        Console.WriteLine($"{Role} 使用了 {Name}。");
        Role.Hp += 150;
        Role.Mp -= MpCost;
    }
}