using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role;

public class Monster : Role
{
    public Monster(Position position, Map map) : base(position, map)
    {
    }

    public Guid Type { get; set; } = Guid.NewGuid();
    public override char Symbol => 'M';

    protected override int FullHp => 1;
    public override int AttackPower { get; }

    protected override Direction ChooseMoveDirection(IEnumerable<Direction> canMoveDirections)
    {
        var random = new Random();
        var next = random.Next(1, canMoveDirections.Count());

        return canMoveDirections.ElementAt(next);
    }


    protected internal override void Attack()
    {
        if (StateEnum == StateEnum.Erupting)
        {
            for (var row = 0; row < Map.MapObjects.GetLength(0); row++)
            {
                for (var column = 0; column < Map.MapObjects.GetLength(1); column++)
                {
                    var mapObject = Map.MapObjects[row, column];
                    if (mapObject is Monster monster)
                    {
                        monster.OnDamaged(50);
                    }
                }
            }

            Console.WriteLine($"位於[{Position.Row},{Position.Column}]的{Symbol}是爆發狀態，全都被攻擊了");
        }

        var character = GetCharacterThatBeAttacked()!;
        var characterPosition = character.Position;

        Console.WriteLine(
            $"在[{Position.Row},{Position.Column}]的{Symbol} 攻擊在 [{characterPosition.Row},{characterPosition.Column}]的{character.Symbol}");
        character.OnDamaged(50);
    }

    public override void OnDamaged(int hp)
    {
        if (StateEnum == StateEnum.Invincible)
        {
            Console.WriteLine($"位於[{Position.Row}{Position.Column}]的{Symbol}是無敵狀態不會被攻擊");
            return;
        }

        if (StateEnum is StateEnum.Accelerated or StateEnum.Stockpile)
        {
            SetState(StateEnum.Normal);
        }

        Hp -= hp;
        if (Hp <= 0)
        {
            Map.RemoveMapObjectAt(Position);
            Console.WriteLine($"一隻在 [{Position.Row},{Position.Column}] 的怪物已死亡，從地圖消失了");
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
        return GetCharacterThatBeAttacked() is not null;
    }


    private Character? GetCharacterThatBeAttacked()
    {
        var mapObjects = Map.MapObjects;

        if (Position.Row > 0)
        {
            var top = new Position(Position.Row - 1, Position.Column);
            if (mapObjects[top.Row, top.Column] is Character topCharacter)
            {
                return topCharacter;
            }
        }

        if (Position.Column < Map.ColumnLimitIndex)
        {
            var right = new Position(Position.Row, Position.Column + 1);
            if (mapObjects[right.Row, right.Column] is Character rightCharacter)
            {
                return rightCharacter;
            }
        }

        if (Position.Row < Map.ColumnLimitIndex)
        {
            var down = new Position(Position.Row + 1, Position.Column);
            if (mapObjects[down.Row, down.Column] is Character downCharacter)
            {
                return downCharacter;
            }
        }

        if (Position.Column > 0)
        {
            var left = new Position(Position.Row, Position.Column - 1);
            if (mapObjects[left.Row, left.Column] is Character leftCharacter)
            {
                return leftCharacter;
            }
        }

        return null;
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