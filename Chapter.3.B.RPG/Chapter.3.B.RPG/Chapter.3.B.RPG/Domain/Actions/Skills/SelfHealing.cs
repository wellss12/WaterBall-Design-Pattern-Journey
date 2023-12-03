using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class SelfHealing : Skill
{
    public override string Name => "自我治療";
    public override int TargetCount => 1;
    public override int MpCost => 50;

    protected override void ExecuteAction(IEnumerable<Role> targets) => Role.Hp += 150;

    public override IEnumerable<Role> GetCandidates() => Enumerable.Empty<Role>();
}