using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills.OnePunchHandlers;

public class HpGreaterThanOrEqualTo500Handler : OnePunchHandler
{
    public HpGreaterThanOrEqualTo500Handler(OnePunchHandler? next) : base(next)
    {
    }

    protected override bool IsMatch(Role attackee) => attackee.Hp >= 500;

    protected override void DoHanding(Role attacker, Role attackee) => attacker.Damage(attackee, 300);
}