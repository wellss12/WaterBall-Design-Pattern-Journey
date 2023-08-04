﻿using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles.States;
using Chapter._3._3.H.寶藏地圖.Domain.Maps;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

/// <summary>
/// 任意門
/// </summary>
class DokodemoDoor : Treasure
{
    public DokodemoDoor(Position position, Map map) : base(position, map)
    {
    }

    protected override Func<Role, State> GetStateOnTouched => role => new TeleportState(role);
}