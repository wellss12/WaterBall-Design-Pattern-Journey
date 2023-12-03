using Chapter._3.B.RPG.Domain.Roles;
using Chapter._3.B.RPG.Domain.States;

namespace Chapter._3.B.RPG.Domain.Actions.Skills.OnePunchHandlers;

public class IsCheerupHandler : OnePunchHandler
{
    public IsCheerupHandler(OnePunchHandler? next) : base(next)
    {
    }

    protected override bool IsMatch(Role attackee) => attackee.State is CheerupState;

    protected override void DoHanding(Role attacker, Role attackee)
    {
        attacker.Damage(attackee, 100);
        attackee.State = new NormalState(attackee);
    }
}