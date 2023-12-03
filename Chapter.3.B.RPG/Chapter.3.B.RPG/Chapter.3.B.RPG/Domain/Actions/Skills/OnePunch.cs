using Chapter._3.B.RPG.Domain.Actions.Skills.OnePunchHandlers;
using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class OnePunch : Skill
{
    public OnePunch(OnePunchHandler handler)
    {
        _handler = handler;
    }

    private readonly OnePunchHandler _handler;
    public override string Name => "一拳攻擊";
    public override int TargetCount => 1;
    public override int MpCost => 180;

    protected override void ExecuteAction(IEnumerable<Role> targets)
    {
        foreach (var target in targets)
        {
            _handler.Handle(Role, target);
        }
    }

    public override IEnumerable<Role> GetCandidates() => Role.Troop.GetAliveEnemies();
}