namespace Chapter._3._3.H.寶藏地圖.Domain.MapObject;

public abstract class MapObject
{
    protected MapObject(Position position, Map.Map map)
    {
        Map = map;
        Position = position;
    }

    protected readonly Map.Map Map;
    public Position Position { get; protected set; }
    public abstract char Symbol { get; }
}