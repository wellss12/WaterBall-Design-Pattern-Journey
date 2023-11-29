using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Waterball : Skill
{
    public override string Name => "水球";
    public override int TargetCount => 1;
    public override int MpCost => 50;
    public override int Str => 120;

    public override void Execute(IEnumerable<Role> targets)
    {
        var targetNames = targets.Select(target => target.ToString());
        var targetNameString = string.Join(", ", targetNames);
        Console.WriteLine($"{Role} 對 {targetNameString} 使用了 {Name}。");

        foreach (var target in targets)
        {
            Console.WriteLine($"{Role} 對 {target} 造成 {Str} 點傷害。");
            target.OnDamaged(Str);
        }

        Role.Mp -= MpCost;
    }

    public override IEnumerable<Role> GetCandidates() => Role.Troop.Battle.GetEnemies(Role);
}