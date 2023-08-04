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
    public override int AttackPower => 1;

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

    public override IEnumerable<Role> GetAttackableRoles()
    {
        var nextPosition = Position;
        while (Map.IsValid(nextPosition))
        {
            nextPosition = nextPosition.GetNextPosition(Direction);
            var mapObject = Map.GetMapObjectAt(nextPosition);

            if (mapObject is Obstacle)
            {
                break;
            }

            if (mapObject is Monster monster)
            {
                yield return monster;
            }
        }
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