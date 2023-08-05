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
        if (HasAttackableCharacterNearby())
        {
            Attack();
        }
        else
        {
            Move();
        }
    }


    private bool HasAttackableCharacterNearby()
    {
        return GetAttackableRoles().Any();
    }
}