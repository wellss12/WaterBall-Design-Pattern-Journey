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
    public virtual int AttackPower => Role.AttackPower;

    internal virtual void PreRoundAction()
    {
        
    }

    internal virtual void RoundAction() => Role.RoundAction();

    internal void EndRoundAction()
    {
        var isNotNormal = this is not NormalState;
        if (isNotNormal && Timeliness > 0)
        {
            Timeliness--;
            if (Timeliness is 0)
            {
                ExitState();
            }
        }
    }

    protected virtual void ExitState() => Role.SetState(GetStateAfterTimeliness());

    internal virtual void OnDamaged(int damage)
    {
        Console.WriteLine($"{Role.Symbol} 已受到攻擊，hp 損失 {damage}");
        Role.Hp -= damage;
        if (Role.IsDead() is false)
        {
            Role.SetState(GetStateAfterOnDamaged());
        }
        else
        {
            Role.Map.RemoveMapObject(Role);
        }
    }

    protected virtual State GetStateAfterTimeliness() => new NormalState(Role);

    protected virtual State GetStateAfterOnDamaged() => new InvincibleState(Role);

    public virtual IEnumerable<Direction> GetCanMoveDirections() => Role.GetCanMoveDirections();

    public virtual IEnumerable<Role> GetAttackableRoles() => Role.GetAttackableRoles();
}