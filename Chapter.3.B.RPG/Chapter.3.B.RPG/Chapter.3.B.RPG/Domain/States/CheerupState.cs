﻿using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.States;

public class CheerupState : State
{
    public CheerupState(Role role) : base(role)
    {
    }

    protected override int Timeliness { get; set; } = 3;
    protected override string Name => "受到鼓舞";
}