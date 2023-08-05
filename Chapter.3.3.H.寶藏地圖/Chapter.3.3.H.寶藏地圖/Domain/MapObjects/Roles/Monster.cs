using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;

public class Monster : Role
{
    public Monster(Position position, Map map) : base(position, map)
    {
    }

    public override char Symbol => 'M';

    protected override int FullHp => 1;
    public override int AttackPower => 50;

    protected override Direction ChooseMoveDirection(IEnumerable<Direction> canMoveDirections)
    {
        var random = new Random();
        var next = random.Next(1, canMoveDirections.Count());

        return canMoveDirections.ElementAt(next);
    }

    protected override IEnumerable<Role> GetAttackableRoles()
    {
        var positions = new List<Position>
        {
            Position.GetNextPosition(Direction.Up),
            Position.GetNextPosition(Direction.Right),
            Position.GetNextPosition(Direction.Down),
            Position.GetNextPosition(Direction.Left)
        };

        var mapObjects = positions
            .Where(position => Map.IsValid(position))
            .Select(position => Map.GetMapObjectAt(position));

        foreach (var mapObject in mapObjects)
        {
            if (mapObject is Character character)
            {
                yield return character;
            }
        }
    }

    public override void RoundAction()
    {
        if (StateEnum == StateEnum.Invincible && StatusStartTurn == 2)
        {
            SetState(StateEnum.Normal);
        }
        else if (StateEnum == StateEnum.Poisoned)
        {
            if (StatusStartTurn == 3)
            {
                SetState(StateEnum.Normal);
            }
            else
            {
                Hp -= 15;
            }
        }
        else if (StateEnum == StateEnum.Accelerated && StatusStartTurn == 3)
        {
            SetState(StateEnum.Normal);
        }
        else if (StateEnum == StateEnum.Healing)
        {
            if (StatusStartTurn == 6 || Hp == 300)
            {
                SetState(StateEnum.Normal);
            }
            else
            {
                Hp += 30;
                if (Hp == 300)
                {
                    SetState(StateEnum.Normal);
                }
            }
        }
        else if (StateEnum == StateEnum.Orderless && StatusStartTurn == 3)
        {
            SetState(StateEnum.Normal);
        }
        else if (StateEnum == StateEnum.Stockpile && StatusStartTurn == 2)
        {
            SetState(StateEnum.Erupting);
        }
        else if (StateEnum == StateEnum.Erupting && StatusStartTurn == 3)
        {
            SetState(StateEnum.Teleport);
        }
        else if (StateEnum == StateEnum.Teleport && StatusStartTurn == 1)
        {
            Move();
            SetState(StateEnum.Normal);
        }
        else if (StateEnum == StateEnum.Accelerated)
        {
            Console.WriteLine("狀態為加速，可以執行兩次動作");
            for (var actionCount = 0; actionCount < 2; actionCount++)
            {
                HandleAction();
            }
        }
        else
        {
            HandleAction();
        }
    }

    private void HandleAction()
    {
        if (StateEnum == StateEnum.Orderless)
        {
            Console.WriteLine("目前為混亂狀態，只能移動");
            Move();
        }

        if (HasAttackableCharacterNearby())
        {
            Attack();
        }
        else
        {
            this.Move();
        }
    }


    private bool HasAttackableCharacterNearby()
    {
        return GetAttackableRoles().Any();
    }

    private Position GetPosition(Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Position(Position.Row - 1, Position.Column),
            Direction.Right => new Position(Position.Row, Position.Column + 1),
            Direction.Down => new Position(Position.Row + 1, Position.Column),
            Direction.Left => new Position(Position.Row, Position.Column - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private static bool IsValidMove(Position position)
    {
        return position is
        {
            Row: >= 0 and <= Map.RowLimitIndex,
            Column: >= 0 and <= Map.ColumnLimitIndex
        };
    }
}