using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;
using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

class Poison : Treasure
{
    public Poison(Position position, Map map) : base(position, map)
    {
    }

    protected override Func<Role, State> GetStateAfterOnTouched { get; } = role => new PoisonedState(role);
    protected override string Name => "毒藥";
}