namespace Chapter._3._3.H.寶藏地圖.Domain.MapObject.Role;

/// <summary>
/// 主角
/// </summary>
public class Character : Role
{
    public Character(MoveDirection direction, Position position, Map.Map map) : base(position, map)
    {
        Direction = Enum.GetValues<MoveDirection>().Contains(direction)
            ? direction
            : throw new Exception("must be '↑', '→', '↓', '←'");
    }

    public override int Hp { get; protected set; } = 300;

    public override char Symbol
    {
        get
        {
            return Direction switch
            {
                MoveDirection.Up => '↑',
                MoveDirection.Right => '→',
                MoveDirection.Down => '↓',
                MoveDirection.Left => '←',
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    private MoveDirection Direction { get; set; }

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

                var allowDirectionList = new List<char>();
                foreach (var allowMoveDirection in allowMoveDirections)
                {
                    switch (allowMoveDirection)
                    {
                        case MoveDirection.Up:
                            allowDirectionList.Add('↑');
                            break;
                        case MoveDirection.Right:
                            allowDirectionList.Add('→');
                            break;
                        case MoveDirection.Down:
                            allowDirectionList.Add('↓');
                            break;
                        case MoveDirection.Left:
                            allowDirectionList.Add('←');
                            break;
                    }
                }

                var allowDirectionMessage = string.Join(',', allowDirectionList);
                Console.WriteLine($"你只能選擇 {allowDirectionMessage}");
                var targetDirection = Console.ReadKey().KeyChar switch
                {
                    '↑' => MoveDirection.Up,
                    '→' => MoveDirection.Right,
                    '↓' => MoveDirection.Down,
                    '←' => MoveDirection.Left,
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (allowMoveDirections.Contains(targetDirection))
                {
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
                else
                {
                    Console.WriteLine($"只能往{allowDirectionMessage}移動");
                    continue;
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
                Console.WriteLine("要往哪個方向移動'↑', '→', '↓', '←'");
                var targetDirection = Console.ReadKey().KeyChar switch
                {
                    '↑' => MoveDirection.Up,
                    '→' => MoveDirection.Right,
                    '↓' => MoveDirection.Down,
                    '←' => MoveDirection.Left,
                    _ => throw new ArgumentOutOfRangeException()
                };

                Console.WriteLine();

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

            break;
        }
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

    protected override void Attack()
    {
        //      row
        //column 0 1 2 3 4 5
        //       1
        //       2
        //       3
        //       4
        //       5

        //角色的攻擊範圍擴充至「全地圖」，且攻擊行為變成「全場攻擊」：
        //每一次攻擊時都會攻擊到地圖中所有其餘角色，且攻擊力為50。三回合過後取得瞬身狀態
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

        if (Direction == MoveDirection.Up)
        {
            for (var row = Position.Row + 1; row <= 0; row--)
            {
                var mapObject = Map.MapObjects[row, Position.Column];
                if (mapObject is Obstacle)
                {
                    Console.WriteLine($"攻擊已被阻擋，在 [{row},{Position.Column}] 上有障礙物，攻擊不能穿越障礙物");
                    break;
                }

                if (mapObject is Monster monster)
                {
                    monster.Damage(1);
                }
            }
        }
        else if (Direction == MoveDirection.Right)
        {
            //TODO: Map.MapArray.GetLength(1) - 1 const
            for (var column = Position.Column + 1; column <= Domain.Map.Map.ColumnLimitIndex; column++)
            {
                var mapObject = Map.MapObjects[Position.Row, column];
                if (mapObject is Obstacle)
                {
                    Console.WriteLine($"攻擊已被阻擋，在 [{Position.Row},{column}] 上有障礙物，攻擊不能穿越障礙物");
                    break;
                }

                if (mapObject is Monster monster)
                {
                    monster.Damage(1);
                    Console.WriteLine($"一隻在 [{Position.Row},{column}] 的怪物已死亡，從地圖消失了");
                }
            }
        }
        else if (Direction == MoveDirection.Down)
        {
            for (var row = Position.Row + 1; row <= Domain.Map.Map.RowLimitIndex; row++)
            {
                var mapObject = Map.MapObjects[row, Position.Column];
                if (mapObject is Obstacle)
                {
                    Console.WriteLine($"攻擊已被阻擋，在 [{row},{Position.Column}] 上有障礙物，攻擊不能穿越障礙物");
                    break;
                }

                if (mapObject is Monster monster)
                {
                    monster.Damage(1);
                    Console.WriteLine($"一隻在 [{row},{Position.Column}] 的怪物已死亡，從地圖消失了");
                }
            }
        }
        else if (Direction == MoveDirection.Left)
        {
            for (var column = Position.Column + 1; column >= 0; column--)
            {
                var mapObject = Map.MapObjects[Position.Row, column];
                if (mapObject is Obstacle)
                {
                    Console.WriteLine($"攻擊已被阻擋，在 [{Position.Row},{column}] 上有障礙物，攻擊不能穿越障礙物");
                    break;
                }

                if (mapObject is Monster monster)
                {
                    monster.Damage(1);
                    Console.WriteLine($"一隻在 [{Position.Row},{column}] 的怪物已死亡，從地圖消失了");
                }
            }
        }

        Console.WriteLine("攻擊結束");
    }

    public override void Damage(int hp)
    {
        if (State == State.Invincible)
        {
            Console.WriteLine("無敵狀態不會被攻擊");
            return;
        }

        if (State is State.Accelerated or State.Stockpile)
        {
            SetState(State.Normal);
        }

        Hp -= hp;
        if (Hp > 0)
        {
            Console.WriteLine($"{Symbol}受到傷害，剩下{Hp}");
            SetState(State.Invincible);
        }
        else
        {
            Map.RemoveMapObjectAt(Position);
            Console.WriteLine($"{Symbol} 在 [{Position.Row},{Position.Column}] 死亡");
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

        if (State == State.Accelerated)
        {
            Console.WriteLine("狀態為加速，可以執行兩次動作");
            for (var actionCount = 0; actionCount < 2; actionCount++)
            {
                HandleAction();
            }
        }

        HandleAction();
    }

    private void HandleAction()
    {
        if (State == State.Orderless)
        {
            Console.WriteLine("目前為混亂狀態，只能移動");
            Move();
        }

        Console.WriteLine(@$"
主角請選擇要執行的動作:
a.往一個方向移動一格 
b.朝當前方向執行攻擊");

        var answer = Console.ReadKey().KeyChar;
        Console.WriteLine();
        if (answer is 'a')
        {
            Move();
        }
        else if (answer is 'b')
        {
            Attack();
        }
    }

    private MoveDirection GetCurrentDirection(Position targetPosition)
    {
        var rowDifference = GetRowDifference(targetPosition);
        var columnDifference = GetColumnDifference(targetPosition);

        return (rowDifference, columnDifference) switch
        {
            (1, 0) => MoveDirection.Down,
            (-1, 0) => MoveDirection.Up,
            (0, 1) => MoveDirection.Right,
            (0, -1) => MoveDirection.Left,
            _ => throw new ArgumentException("Invalid move")
        };
    }

    private int GetRowDifference(Position targetPosition)
    {
        return targetPosition.Row - Position.Row;
    }

    private int GetColumnDifference(Position targetPosition)
    {
        return targetPosition.Column - Position.Column;
    }
}