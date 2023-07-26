namespace Chapter._3._3.H.寶藏地圖.Domain.MapObject.Role;

public class Monster : Role
{
    public Monster(Position position, Map.Map map) : base(position, map)
    {
    }

    public Guid Type { get; set; } = Guid.NewGuid();
    public override char Symbol => 'M';

    public override int Hp { get; protected set; } = 1;

    protected override void Move()
    {
        while (true)
        {
            if (State == State.Orderless)
            {
                var random = new Random();
                var next = random.Next(0, 1);
                var allowMoveDirections = next switch
                {
                    0 => new List<MoveDirection> {MoveDirection.Up, MoveDirection.Down},
                    1 => new List<MoveDirection> {MoveDirection.Left, MoveDirection.Right}
                };

                var targetDirection = random.Next(0, 1) == 0
                    ? allowMoveDirections[0]
                    : allowMoveDirections[1];

                var targetPosition = GetPosition(targetDirection);
                if (IsValidMove(targetPosition) is false)
                {
                    Console.WriteLine($"在[{Position.Row},{Position.Column}]的{Symbol}已經超過地圖邊界了，請往別的方向移動");
                    continue;
                }

                var mapObject = Map.MapObjects[targetPosition.Row, targetPosition.Column];
                if (mapObject is not null)
                {
                    Touch(mapObject);
                }
                else
                {
                    Map.RemoveMapObjectAt(Position);
                    Map.MapObjects[targetPosition.Row, targetPosition.Column] = this;
                    Position = targetPosition;
                    Console.WriteLine(
                        $"{Symbol}成功從 [{Position.Row},{Position.Column}] 移動到 [{targetPosition.Row},{targetPosition.Column}]");
                }
            }
            else if (State == State.Teleport && StatusStartTurn == 1)
            {
                for (var row = 0; row < Map.MapObjects.GetLength(0); row++)
                {
                    for (var column = 0; column < Map.MapObjects.GetLength(1); column++)
                    {
                        if (Map.MapObjects[row, column] is null)
                        {
                            Map.RemoveMapObjectAt(Position);
                            Map.MapObjects[row, column] = this;
                            Position = new Position(row, column);
                            Console.WriteLine(
                                $"{Symbol}被隨機移動到空地:[{row},{column}]");
                        }
                    }
                }
            }
            else
            {
                var random = new Random();
                var next = random.Next(0, 3);
                var targetDirection = next switch
                {
                    0 => MoveDirection.Up,
                    1 => MoveDirection.Right,
                    2 => MoveDirection.Down,
                    3 => MoveDirection.Left,
                    _ => throw new ArgumentOutOfRangeException()
                };

                var targetPosition = GetPosition(targetDirection);

                if (IsValidMove(targetPosition) is false)
                {
                    Console.WriteLine($"在 [{Position.Row},{Position.Column}] 的{Symbol}已經超過地圖邊界了，請往別的方向移動");
                    continue;
                }

                var mapObject = Map.MapObjects[targetPosition.Row, targetPosition.Column];
                if (mapObject is not null)
                {
                    Touch(mapObject);
                }
                else
                {
                    Map.RemoveMapObjectAt(Position);
                    Map.MapObjects[targetPosition.Row, targetPosition.Column] = this;
                    Position = targetPosition;
                    Console.WriteLine(
                        $"{Symbol}成功從 [{Position.Row},{Position.Column}] 移動到 [{targetPosition.Row},{targetPosition.Column}]");
                }
            }

            break;
        }
    }

    protected override void Attack()
    {
        if (State == State.Erupting)
        {
            for (var row = 0; row < Map.MapObjects.GetLength(0); row++)
            {
                for (var column = 0; column < Map.MapObjects.GetLength(1); column++)
                {
                    var mapObject = Map.MapObjects[row, column];
                    if (mapObject is Monster monster)
                    {
                        monster.Damage(50);
                    }
                }
            }

            Console.WriteLine($"位於[{Position.Row},{Position.Column}]的{Symbol}是爆發狀態，全都被攻擊了");
        }

        var character = GetCharacterThatBeAttacked()!;
        var characterPosition = character.Position;

        Console.WriteLine(
            $"在[{Position.Row},{Position.Column}]的{Symbol} 攻擊在 [{characterPosition.Row},{characterPosition.Column}]的{character.Symbol}");
        character.Damage(50);
    }

    public override void Damage(int hp)
    {
        if (State == State.Invincible)
        {
            Console.WriteLine($"位於[{Position.Row}{Position.Column}]的{Symbol}是無敵狀態不會被攻擊");
            return;
        }

        if (State is State.Accelerated or State.Stockpile)
        {
            SetState(State.Normal);
        }

        Hp -= hp;
        if (Hp <= 0)
        {
            Map.RemoveMapObjectAt(Position);
            Console.WriteLine($"一隻在 [{Position.Row},{Position.Column}] 的怪物已死亡，從地圖消失了");
        }
    }

    public override void TakeTurn()
    {
        if (State == State.Invincible && StatusStartTurn == 2)
        {
            SetState(State.Normal);
        }
        else if (State == State.Poisoned)
        {
            if (StatusStartTurn == 3)
            {
                SetState(State.Normal);
            }
            else
            {
                Hp -= 15;
            }
        }
        else if (State == State.Accelerated && StatusStartTurn == 3)
        {
            SetState(State.Normal);
        }
        else if (State == State.Healing)
        {
            if (StatusStartTurn == 6 || Hp == 300)
            {
                SetState(State.Normal);
            }
            else
            {
                Hp += 30;
                if (Hp == 300)
                {
                    SetState(State.Normal);
                }
            }
        }
        else if (State == State.Orderless && StatusStartTurn == 3)
        {
            SetState(State.Normal);
        }
        else if (State == State.Stockpile && StatusStartTurn == 2)
        {
            SetState(State.Erupting);
        }
        else if (State == State.Erupting && StatusStartTurn == 3)
        {
            SetState(State.Teleport);
        }
        else if (State == State.Teleport && StatusStartTurn == 1)
        {
            Move();
            SetState(State.Normal);
        }
        else if (State == State.Accelerated)
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
        if (State == State.Orderless)
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

        if (Position.Column < Domain.Map.Map.ColumnLimitIndex)
        {
            var right = new Position(Position.Row, Position.Column + 1);
            if (mapObjects[right.Row, right.Column] is Character rightCharacter)
            {
                return rightCharacter;
            }
        }

        if (Position.Row < Domain.Map.Map.ColumnLimitIndex)
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

    private Position GetPosition(MoveDirection direction)
    {
        return direction switch
        {
            MoveDirection.Up => new Position(Position.Row - 1, Position.Column),
            MoveDirection.Right => new Position(Position.Row, Position.Column + 1),
            MoveDirection.Down => new Position(Position.Row + 1, Position.Column),
            MoveDirection.Left => new Position(Position.Row, Position.Column - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private static bool IsValidMove(Position position)
    {
        return position is
        {
            Row: >= 0 and <= Domain.Map.Map.RowLimitIndex,
            Column: >= 0 and <= Domain.Map.Map.ColumnLimitIndex
        };
    }
}