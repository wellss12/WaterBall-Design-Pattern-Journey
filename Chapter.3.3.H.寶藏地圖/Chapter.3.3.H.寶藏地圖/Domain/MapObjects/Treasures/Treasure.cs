using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;
using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

/// <summary>
/// 寶物
/// </summary>
public abstract class Treasure : MapObject
{
    protected Treasure(Position position, Map map) : base(position, map)
    {
    }

    protected abstract Func<Role, State> GetStateAfterOnTouched { get; }
    protected abstract string Name { get; }
    public override char Symbol => 'x';

    protected internal override void OnTouched(MapObject mapObject)
    {
        var role = mapObject as Role;
        Console.WriteLine($"位於 {role.Position} 的 {role.Symbol} 撞到{Name}拉");
        role.SetState(GetStateAfterOnTouched(role));
        Map.RemoveMapObject(this);
    }
}