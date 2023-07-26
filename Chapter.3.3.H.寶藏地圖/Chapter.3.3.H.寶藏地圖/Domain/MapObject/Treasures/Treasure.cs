namespace Chapter._3._3.H.寶藏地圖.Domain.MapObject.Treasures;

/// <summary>
/// 寶物
/// </summary>
abstract class Treasure : MapObject
{
    protected Treasure(Position position, Map.Map map) : base(position, map)
    {
    }

    public override char Symbol => 'x';
}