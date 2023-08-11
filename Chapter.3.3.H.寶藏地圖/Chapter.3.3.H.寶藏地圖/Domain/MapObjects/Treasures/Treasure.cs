using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;
using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

/// <summary>
/// 寶物
/// </summary>
abstract class Treasure : MapObject
{
    protected abstract Func<Role, State> GetStateOnTouched { get; }

    protected Treasure(Position position, Map map) : base(position, map)
    {
    }

    public override char Symbol => 'x';

    protected override void OnTouched(MapObject mapObject)
    {
        var role = mapObject as Role;
        role.SetState(GetStateOnTouched(role));
        Map.RemoveMapObject(this);
    }
}