using Chapter._3.B.RPG.Domain.Roles;
using Chapter._3.B.RPG.Domain.States;

namespace Chapter._3.B.RPG.Domain.Actions.Skills.OnePunchHandlers;

public class IsPoisonedOrPetrochemicalHandler : OnePunchHandler
{
    public IsPoisonedOrPetrochemicalHandler(OnePunchHandler? next) : base(next)
    {
    }

    protected override bool IsMatch(Role target) => target.State is PoisonedState or PetrochemicalState;

    protected override void DoHanding(Role attacker, Role attackee)
    {
        for (var i = 0; i < 3; i++)
        {
            if (attackee.IsAlive())
            {
                attacker.Damage(attackee, 80);
            }
        }
    }
}