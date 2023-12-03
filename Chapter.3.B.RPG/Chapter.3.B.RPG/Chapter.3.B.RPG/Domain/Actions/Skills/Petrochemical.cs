using Chapter._3.B.RPG.Domain.Roles;
using Chapter._3.B.RPG.Domain.States;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Petrochemical : Skill
{
    public override string Name => "石化";
    public override int TargetCount => 1;
    public override int MpCost => 100;

    protected override void ExecuteAction(IEnumerable<Role> targets)
    {
        foreach (var target in targets)
        {
            target.State = new PetrochemicalState(target);
        }
    }

    public override IEnumerable<Role> GetCandidates() => Role.Troop.GetAliveEnemies();
}