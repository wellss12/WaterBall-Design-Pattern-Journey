using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;
using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;

public abstract class Role : MapObject
{
    protected Role(Position position, Map map) : base(position, map)
    {
        State = new NormalState(this);
        Hp = FullHp;
    }

    protected internal abstract int AttackPower { get; }
    public int Hp { get; set; }
    public State State { get; private set; }
    protected abstract int FullHp { get; }
    public abstract void RoundAction();
    protected abstract Direction ChooseMoveDirection(IEnumerable<Direction> canMoveDirections);
    protected internal abstract IEnumerable<Role> GetAttackableRoles();

    protected internal void Move()
    {
        while (true)
        {
            var canMoveDirections = State.GetCanMoveDirections();
            var targetDirection = ChooseMoveDirection(canMoveDirections);
            var targetPosition = Position.GetNextPosition(targetDirection);

            if (IsValidMove(canMoveDirections, targetDirection))
            {
                var mapObject = Map.GetMapObjectAt(targetPosition);
                if (mapObject is not null)
                {
                    mapObject.OnTouched(this);
                }
                else
                {
                    Map.Move(this, targetPosition);
                }
            }
            else
            {
                Console.WriteLine("移動方向無效，請往別的方向移動");
                continue;
            }

            break;
        }
    }

    protected void Attack()
    {
        //      row
        //column 0 1 2 3 4 5
        //       1
        //       2
        //       3
        //       4
        //       5

        foreach (var attackableRole in State.GetAttackableRoles())
        {
            Console.WriteLine($"在 {Position} 的 {Symbol} 攻擊在 {attackableRole.Position} 的 {attackableRole.Symbol}");
            attackableRole.OnDamaged(State.AttackPower);
        }

        Console.WriteLine($"{Symbol} 攻擊結束!");
    }

    public void RoundStart()
    {
        State.PreRoundAction();
        State.RoundAction();
        State.EndRoundAction();
    }


    private void OnDamaged(int attackPower) => State.OnDamaged(attackPower);

    public bool IsDead() => Hp <= 0;

    public bool IsFullHp() => Hp >= FullHp;

    public void SetState(State state)
    {
        Console.WriteLine($"從{State.Name}變成{state.Name}");
        State = state;
    }

    public IEnumerable<Direction> GetCanMoveDirections()
    {
        if (Map.IsValid(new Position(Position.Row - 1, Position.Column)))
        {
            yield return Direction.Up;
        }

        if (Map.IsValid(new Position(Position.Row, Position.Column + 1)))
        {
            yield return Direction.Right;
        }

        if (Map.IsValid(new Position(Position.Row + 1, Position.Column)))
        {
            yield return Direction.Down;
        }

        if (Map.IsValid(new Position(Position.Row, Position.Column - 1)))
        {
            yield return Direction.Left;
        }
    }

    private bool IsValidMove(IEnumerable<Direction> canMoveDirections, Direction targetDirection)
    {
        return canMoveDirections.Any(direction => direction == targetDirection);
    }

    protected bool HasAttackableRoles()
    {
        return GetAttackableRoles().Any();
    }
}