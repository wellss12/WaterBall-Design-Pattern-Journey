using Chapter._3.B.RPG.Domain.Roles;
using Chapter._3.B.RPG.Domain.States;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

internal class Cheerup : Skill
{
    public override string Name => "鼓舞";
    public override int TargetCount => 3;
    public override int MpCost => 100;

    protected override void Action(IEnumerable<Role> targets)
    {
        foreach (var target in targets)
        {
            target.State = new CheerupState(target);
        }
    }

    public override IEnumerable<Role> GetCandidates()
    {
        return Role.Troop.Battle.GetAllies(Role).Where(role => role != Role);
    }
}