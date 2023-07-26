namespace Chapter._3._3.H.寶藏地圖.Domain.MapObject;

/// <summary>
/// 障礙物
/// </summary>
class Obstacle : MapObject
{
    public Obstacle(Position position, Map.Map map) : base(position, map)
    {
    }

    public override char Symbol => '□';
}