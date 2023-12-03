using Chapter._3.B.RPG.Domain.Observers;
using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Curse : Skill
{
    public override string Name => "詛咒";
    public override int TargetCount => 1;
    public override int MpCost => 100;

    protected override void ExecuteAction(IEnumerable<Role> targets)
    {
        var cursedTargets = targets.Where(target => target.RoleDeadObservers
            .Any(observer => observer is CurseObserver curseObserver && curseObserver.Cursator == Role));

        var unCursedTargets = targets.Except(cursedTargets);

        foreach (var target in unCursedTargets)
        {
            target.Register(new CurseObserver(Role));
        }
    }

    public override IEnumerable<Role> GetCandidates() => Role.Troop.GetAliveEnemies();
}