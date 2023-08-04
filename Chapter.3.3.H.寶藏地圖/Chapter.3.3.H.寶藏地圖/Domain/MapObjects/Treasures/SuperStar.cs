using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;
using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

/// <summary>
/// 無敵星星
/// </summary>
class SuperStar : Treasure
{
    public SuperStar(Position position, Map map) : base(position, map)
    {
    }

    protected override Func<Role, State> GetStateOnTouched => role => new InvincibleState(role);
}