﻿using Chapter._3.B.RPG.Domain.Roles;
using Chapter._3.B.RPG.Domain.States;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

internal class Cheerup : Skill
{
    public override string Name => "鼓舞";
    public override int TargetCount => 3;
    public override int MpCost => 100;
    public override int Str => 0;
    public override void Execute(IEnumerable<Role> targets)
    {
        var targetNames = targets.Select(target => target.ToString());
        var targetNameString = string.Join(", ", targetNames);
        Console.WriteLine($"{Role} 對 {targetNameString} 使用了 {Name}。");

        foreach (var target in targets)
        {
            target.State = new CheerupState(target);
        }
        
        Role.Mp -= MpCost;
    }

    public override IEnumerable<Role> GetCandidates() 
        => Role.Troop.Battle.GetAllies(Role).Where(role => role != Role);
}