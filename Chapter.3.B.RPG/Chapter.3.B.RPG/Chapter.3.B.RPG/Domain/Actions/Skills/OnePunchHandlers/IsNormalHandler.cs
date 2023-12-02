using Chapter._3.B.RPG.Domain.Roles;
using Chapter._3.B.RPG.Domain.States;

namespace Chapter._3.B.RPG.Domain.Actions.Skills.OnePunchHandlers;

public class IsNormalHandler : OnePunchHandler
{
    public IsNormalHandler(OnePunchHandler? next) : base(next)
    {
    }

    protected override bool IsMatch(Role target) => target.State is NormalState;

    protected override void DoHanding(Role attacker, Role attackee)
    {
        attacker.Damage(attackee, 100);
    }
}