﻿namespace Chapter._3.B.RPG.Domain.States;

public class PoisonedState : State
{
    public PoisonedState(Role role) : base(role)
    {
    }

    protected override int Timeliness { get; set; } = 3;

    protected override string Name => "中毒";
}