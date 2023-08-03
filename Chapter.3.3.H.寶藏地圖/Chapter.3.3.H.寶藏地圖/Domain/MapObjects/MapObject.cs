using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects;

public abstract class MapObject
{
    protected MapObject(Position position, Map map)
    {
        Map = map;
        Position = position;
    }

    public readonly Map Map;
    public Position Position { get; protected internal set; }
    public abstract char Symbol { get; }
}