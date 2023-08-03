namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role.States;

public abstract class State
{
    protected readonly Role Role;

    /// <summary>
    /// 每個狀態回合到要改變的狀態是什麼?
    /// </summary>
    /// <param name="role"></param>
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

    protected virtual void PreRoundAction()
    {
    }

    protected virtual void RoundAction()
    {
        Role.RoundAction();
    }

    protected virtual void EndRoundAction()
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

    protected virtual void OnDamaged(Role attacker)
    {
        Role.Hp -= attacker.AttackPower;
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
    
    protected virtual void Move()
    {
        Role.Move();
    }
}