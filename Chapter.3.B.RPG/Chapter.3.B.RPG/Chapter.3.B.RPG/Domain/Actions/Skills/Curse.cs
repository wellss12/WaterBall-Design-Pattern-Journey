using Chapter._3.B.RPG.Domain.Observers;
using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

internal class Curse : Skill
{
    public override string Name => "詛咒";
    public override int TargetCount => 1;
    public override int MpCost => 100;
    public override int Str => 0;
    public override void Execute(IEnumerable<Role> targets)
    {
        var targetNames = targets.Select(target => target.ToString());
        var targetNameString = string.Join(", ", targetNames);
        Console.WriteLine($"{Role} 對 {targetNameString} 使用了 {Name}。");
        
        var enumerable = targets.Where(target =>
        {
            var curseObservers = target.RoleDeadObservers
                .Where(t => t is CurseObserver)
                .Cast<CurseObserver>();
            return curseObservers.All(observer => observer.Cursator != Role);
        });
        
        foreach (var target in enumerable)
        {
            target.Register(new CurseObserver(Role));    
        }
        
        Role.Mp -= MpCost;
    }

    public override IEnumerable<Role> GetCandidates() => Role.Troop.Battle.GetEnemies(Role);
}