using Chapter._3.B.RPG.Domain.Roles;
using Chapter._3.B.RPG.Domain.States;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Poison : Skill
{
    public override string Name => "下毒";
    public override int TargetCount => 1;
    public override int MpCost => 80;

    protected override void Action(IEnumerable<Role> targets)
    {
        foreach (var target in targets)
        {
            target.State = new PoisonedState(target);
        }
    }

    public override IEnumerable<Role> GetCandidates() => Role.Troop.Battle.GetEnemies(Role);
}