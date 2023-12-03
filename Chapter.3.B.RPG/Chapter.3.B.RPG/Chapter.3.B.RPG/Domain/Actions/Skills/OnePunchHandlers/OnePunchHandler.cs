using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills.OnePunchHandlers;

public abstract class OnePunchHandler
{
    private readonly OnePunchHandler? _next;

    protected OnePunchHandler(OnePunchHandler? next)
    {
        _next = next;
    }

    protected abstract bool IsMatch(Role attackee);
    protected abstract void DoHanding(Role attacker, Role attackee);

    public void Handle(Role attacker, Role attackee)
    {
        if (IsMatch(attackee))
        {
            DoHanding(attacker, attackee);
        }
        else
        {
            _next?.Handle(attacker, attackee);
        }
    }
}