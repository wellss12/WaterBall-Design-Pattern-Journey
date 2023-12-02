using Chapter._3.B.RPG.Domain.Observers;
using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Curse : Skill
{
    public override string Name => "詛咒";
    public override int TargetCount => 1;
    public override int MpCost => 100;

    protected override void Action(IEnumerable<Role> targets)
    {
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
    }

    public override IEnumerable<Role> GetCandidates()
    {
        return Role.Troop.Battle.GetEnemies(Role);
    }
}