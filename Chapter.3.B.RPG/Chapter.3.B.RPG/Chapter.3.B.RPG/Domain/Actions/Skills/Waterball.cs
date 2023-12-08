using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Waterball : Skill
{
    public override string Name => "水球";
    public override int TargetCount => 1;
    public override int MpCost => 50;

    protected override void ExecuteAction(IEnumerable<Role> targets)
    {
        foreach (var target in targets)
        {
            Role.Damage(target, 120);
        }
    }

    public override IEnumerable<Role> GetCandidates() => Role.Troop.GetAliveEnemies();
}