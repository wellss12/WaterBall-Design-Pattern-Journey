using Chapter._3.B.RPG.Domain.Observers;
using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Summon : Skill
{
    public override string Name => "召喚";
    public override int TargetCount => 0;
    public override int MpCost => 150;
    public override int Str => 0;

    public override void Execute(IEnumerable<Role> targets)
    {
        Console.WriteLine($"{Role} 使用了 {Name}。");
        var summon = new Slime();
        Role.Troop.Join(summon);
        summon.Register(new SlimeObserver(Role));
        Role.Mp -= MpCost;
    }

    public override IEnumerable<Role> GetCandidates() => Enumerable.Empty<Role>();
}