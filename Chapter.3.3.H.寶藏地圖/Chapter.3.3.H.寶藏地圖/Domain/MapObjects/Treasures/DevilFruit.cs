using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;
using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

class DevilFruit : Treasure
{
    public DevilFruit(Position position, Map map) : base(position, map)
    {
    }

    protected override Func<Role, State> GetStateAfterOnTouched => role => new OrderlessState(role);
    protected override string Name => "惡魔果實";
}