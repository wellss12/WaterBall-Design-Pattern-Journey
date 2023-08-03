﻿using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

/// <summary>
/// 寶物
/// </summary>
abstract class Treasure : MapObject
{
    protected Treasure(Position position, Map map) : base(position, map)
    {
    }

    public override char Symbol => 'x';
}