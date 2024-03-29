﻿using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Observers;

public class SlimeObserver : IRoleDeadObserver
{
    private readonly Role _summoner;

    public SlimeObserver(Role summoner)
    {
        _summoner = summoner;
    }

    public void Update(Role role)
    {
        if (role.Name == "Slime" && _summoner.IsAlive())
        {
            _summoner.Hp += 30;
        }
    }
}