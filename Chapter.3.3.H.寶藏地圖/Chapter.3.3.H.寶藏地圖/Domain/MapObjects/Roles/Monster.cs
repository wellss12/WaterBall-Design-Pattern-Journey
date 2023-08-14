using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;

public class Monster : Role
{
    public Monster(Position position, Map map) : base(position, map)
    {
    }

    public override char Symbol => 'M';
    protected override int FullHp => 1;
    protected internal override int AttackPower => 50;

    protected override Direction ChooseMoveDirection(IEnumerable<Direction> canMoveDirections)
    {
        var next = new Random().Next(0, canMoveDirections.Count());
        return canMoveDirections.ElementAt(next);
    }

    protected internal override IEnumerable<Role> GetAttackableRoles()
    {
        var validPositions = new List<Position>
        {
            Position.GetNextPosition(Direction.Up),
            Position.GetNextPosition(Direction.Right),
            Position.GetNextPosition(Direction.Down),
            Position.GetNextPosition(Direction.Left)
        }.Where(Map.IsValid);

        var character = validPositions
            .Select(position => Map.GetMapObjectAt(position))
            .SingleOrDefault(mapObject => mapObject is Character);

        return character is not null
            ? new List<Role> {(character as Role)!}
            : Enumerable.Empty<Role>();
    }

    public override void RoundAction()
    {
        if (HasAttackableRoles())
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

}