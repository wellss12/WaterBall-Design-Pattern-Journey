using Chapter._3.B.RPG.Domain.Observers;
using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Summon : Skill
{
    public override string Name => "召喚";
    public override int MpCost => 150;

    protected override void Action(IEnumerable<Role> targets)
    {
        var summon = new Slime();
        Role.Troop.Join(summon);
        summon.Register(new SlimeObserver(Role));
    }

    public override IEnumerable<Role> GetCandidates() => Enumerable.Empty<Role>();
}