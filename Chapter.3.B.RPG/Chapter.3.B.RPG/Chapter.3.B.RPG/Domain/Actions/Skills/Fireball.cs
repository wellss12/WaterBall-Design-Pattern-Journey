using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Fireball : Skill
{
    public override string Name => "火球";
    public override int TargetCount => GetCandidates().Count();
    public override int MpCost => 50;
    public override int Str => 50;

    protected override void ExecuteAction(IEnumerable<Role> targets)
    {
        foreach (var target in targets)
        {
            Role.Damage(target, Str);
        }
    }

    public override IEnumerable<Role> GetCandidates() => Role.Troop.GetAliveEnemies();
}