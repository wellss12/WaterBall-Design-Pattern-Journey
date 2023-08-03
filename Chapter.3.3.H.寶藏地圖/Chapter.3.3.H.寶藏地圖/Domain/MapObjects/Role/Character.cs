using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role;

/// <summary>
/// 主角
/// </summary>
public class Character : Role
{
    public Character(Direction direction, Position position, Map map) : base(position, map)
    {
        Direction = Enum.GetValues<Direction>().Contains(direction)
            ? direction
            : throw new Exception("must be '↑', '→', '↓', '←'");
    }

    protected override int FullHp => 300;
    public override int AttackPower { get; }

    public override char Symbol
    {
        get
        {
            return Direction switch
            {
                Direction.Up => '↑',
                Direction.Right => '→',
                Direction.Down => '↓',
                Direction.Left => '←',
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    private Direction Direction { get; }

    protected override Direction ChooseMoveDirection(IEnumerable<Direction> canMoveDirections)
    {
        var directionNames = canMoveDirections.Select(t => t.GetDescription());
        var descriptionsMessage = string.Join(',', directionNames);

        Console.WriteLine($"只會列出可移動的方向，選擇要往哪個方向移動:{descriptionsMessage}");
        var targetDirection = Console.ReadKey().KeyChar switch
        {
            '↑' => Direction.Up,
            '→' => Direction.Right,
            '↓' => Direction.Down,
            '←' => Direction.Left,
            _ => throw new ArgumentOutOfRangeException()
        };
        Console.WriteLine();

        return targetDirection;
    }

    protected internal override void Attack()
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

        if (Direction == Direction.Up)
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
                    monster.OnDamaged(1);
                }
            }
        }
        else if (Direction == Direction.Right)
        {
            //TODO: Map.MapArray.GetLength(1) - 1 const
            for (var column = Position.Column + 1; column <= Map.ColumnLimitIndex; column++)
            {
                var mapObject = Map.MapObjects[Position.Row, column];
                if (mapObject is Obstacle)
                {
                    Console.WriteLine($"攻擊已被阻擋，在 [{Position.Row},{column}] 上有障礙物，攻擊不能穿越障礙物");
                    break;
                }

                if (mapObject is Monster monster)
                {
                    monster.OnDamaged(1);
                    Console.WriteLine($"一隻在 [{Position.Row},{column}] 的怪物已死亡，從地圖消失了");
                }
            }
        }
        else if (Direction == Direction.Down)
        {
            for (var row = Position.Row + 1; row <= Map.RowLimitIndex; row++)
            {
                var mapObject = Map.MapObjects[row, Position.Column];
                if (mapObject is Obstacle)
                {
                    Console.WriteLine($"攻擊已被阻擋，在 [{row},{Position.Column}] 上有障礙物，攻擊不能穿越障礙物");
                    break;
                }

                if (mapObject is Monster monster)
                {
                    monster.OnDamaged(1);
                    Console.WriteLine($"一隻在 [{row},{Position.Column}] 的怪物已死亡，從地圖消失了");
                }
            }
        }
        else if (Direction == Direction.Left)
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
                    monster.OnDamaged(1);
                    Console.WriteLine($"一隻在 [{Position.Row},{column}] 的怪物已死亡，從地圖消失了");
                }
            }
        }

        Console.WriteLine("攻擊結束");
    }

    public override void OnDamaged(int hp)
    {
        if (StateEnum == StateEnum.Invincible)
        {
            Console.WriteLine("無敵狀態不會被攻擊");
            return;
        }

        if (StateEnum is StateEnum.Accelerated or StateEnum.Stockpile)
        {
            SetState(StateEnum.Normal);
        }

        Hp -= hp;
        if (Hp > 0)
        {
            Console.WriteLine($"{Symbol}受到傷害，剩下{Hp}");
            SetState(StateEnum.Invincible);
        }
        else
        {
            Map.RemoveMapObjectAt(Position);
            Console.WriteLine($"{Symbol} 在 [{Position.Row},{Position.Column}] 死亡");
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

        if (StateEnum == StateEnum.Accelerated)
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
        if (StateEnum == StateEnum.Orderless)
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
}