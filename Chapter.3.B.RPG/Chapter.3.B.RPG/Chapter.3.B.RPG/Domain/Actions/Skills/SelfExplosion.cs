using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

internal class SelfExplosion : Skill
{
    public override string Name => "自爆";
    public override int TargetCount => GetCandidates().Count();
    public override int MpCost => 200;
    public override int Str => 150;
    public override void Execute(IEnumerable<Role> targets)
    {
        var targetNames = targets.Select(target => target.ToString());
        var targetNameString = string.Join(", ", targetNames);
        Console.WriteLine($"{Role} 對 {targetNameString} 使用了 {Name}。");
        
        var enumerable = targets.Where(target => target != Role);
        foreach (var target in enumerable)
        {
            Console.WriteLine($"{Role} 對 {target} 造成 {Str} 點傷害。");
            target.OnDamaged(150);
        }
        
        Role.Mp -= MpCost;
        Role.OnDamaged(Role.Hp);
    }

    public override IEnumerable<Role> GetCandidates()
        => Role.Troop.Battle
            .GetAllRoles()
            .Where(role => role != Role);
}