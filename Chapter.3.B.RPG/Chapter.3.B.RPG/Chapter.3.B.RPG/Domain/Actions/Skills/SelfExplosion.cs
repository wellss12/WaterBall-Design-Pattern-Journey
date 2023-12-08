using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class SelfExplosion : Skill
{
    public override string Name => "自爆";
    public override int TargetCount => GetCandidates().Count();
    public override int MpCost => 200;

    protected override void ExecuteAction(IEnumerable<Role> targets)
    {
        var enumerable = targets.Where(target => target != Role);
        foreach (var target in enumerable)
        {
            Role.Damage(target, 150);
        }

        CommitSuicide();
    }

    private void CommitSuicide()
    {
        Role.OnDamaged(Role.Hp);
    }

    public override IEnumerable<Role> GetCandidates()
    {
        var troop = Role.Troop;
        return troop
            .GetAliveAllies()
            .Union(troop.GetAliveEnemies())
            .Where(role => role != Role);
    }
}