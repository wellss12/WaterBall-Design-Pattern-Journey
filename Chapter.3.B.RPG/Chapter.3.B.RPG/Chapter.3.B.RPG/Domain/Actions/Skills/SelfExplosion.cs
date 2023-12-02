using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class SelfExplosion : Skill
{
    public override string Name => "自爆";
    public override int TargetCount => GetCandidates().Count();
    public override int MpCost => 200;
    public override int Str => 150;

    protected override void Action(IEnumerable<Role> targets)
    {
        var enumerable = targets.Where(target => target != Role);
        foreach (var target in enumerable)
        {
            Role.Damage(target, Str);
        }
        Role.OnDamaged(Role.Hp);
    }

    public override IEnumerable<Role> GetCandidates()
        => Role.Troop.Battle
            .GetAllRoles()
            .Where(role => role != Role);
}