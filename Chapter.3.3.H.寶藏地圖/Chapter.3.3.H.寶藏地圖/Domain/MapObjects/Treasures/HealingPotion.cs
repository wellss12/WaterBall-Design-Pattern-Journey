using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;
using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

/// <summary>
/// 補血罐
/// </summary>
class HealingPotion : Treasure
{
    public HealingPotion(Position position, Map map) : base(position, map)
    {
    }

    protected override Func<Role, State> GetStateOnTouched => role => new HealingState(role);
}