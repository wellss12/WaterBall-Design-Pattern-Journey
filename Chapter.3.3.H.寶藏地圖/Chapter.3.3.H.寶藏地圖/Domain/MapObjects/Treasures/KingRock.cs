using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;
using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

/// <summary>
/// 王者之印
/// </summary>
class KingRock : Treasure
{
    public KingRock(Position position, Map map) : base(position, map)
    {
    }

    protected override Func<Role, State> GetStateOnTouched => role => new StockpileState(role);
}