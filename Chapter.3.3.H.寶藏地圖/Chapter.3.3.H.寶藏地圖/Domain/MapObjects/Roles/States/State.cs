namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;

public abstract class State
{
    protected readonly Role Role;

    protected State(Role role)
    {
        Role = role;
    }

    public abstract string Name { get; }
    protected abstract int Timeliness { get; set; }

    public void RoundStart()
    {
        PreRoundAction();
        RoundAction();
        EndRoundAction();
    }

    protected internal virtual void PreRoundAction()
    {
    }

    protected internal virtual void RoundAction()
    {
        Role.RoundAction();
    }

    protected internal virtual void EndRoundAction()
    {
        var isNotNormal = this is not NormalState;
        if (isNotNormal && Timeliness > 0)
        {
            Timeliness--;
            if (Timeliness is 0)
            {
                Role.SetState(GetStateAfterTimeliness());
            }
        }
    }

    internal virtual void OnDamaged(int damage)
    {
        Role.Hp -= damage;
        if (Role.IsDead() is false)
        {
            Role.SetState(new InvincibleState(Role));
        }
        else
        {
            Role.Map.RemoveMapObjectAt(Role.Position);
        }
    }

    protected virtual State GetStateAfterTimeliness() => new NormalState(Role);
    protected virtual IEnumerable<Direction> GetCanMoveDirections()
    {
        return Role.GetCanMoveDirections();
    }

    protected virtual void Attack()
    {
        Role.Attack();
    }
}