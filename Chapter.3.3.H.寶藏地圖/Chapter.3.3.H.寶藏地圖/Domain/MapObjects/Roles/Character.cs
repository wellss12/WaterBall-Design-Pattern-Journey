using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;

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
    protected override int AttackPower => 1;

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

    protected override IEnumerable<Role> GetAttackableRoles()
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

    public override void RoundAction()
    {
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