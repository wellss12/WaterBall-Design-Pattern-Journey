﻿using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions.Skills;

public class Fireball : Skill
{
    public override string Name => "火球";
    public override int TargetCount => Role.Troop.Battle.GetEnemies(Role).Count();
    public override int MpCost => 50;
    public override int Str => 50;

    public override void Execute(IEnumerable<Role> targets)
    {
        var targetNames = targets.Select(target => target.ToString());
        var targetNameString = string.Join(", ", targetNames);
        Console.WriteLine($"{Role} 對 {targetNameString} 使用了 {Name}。");

        foreach (var target in targets)
        {
            Role.Damage(target, Str);
        }

        Role.Mp -= MpCost;
    }

    public override IEnumerable<Role> GetCandidates() => Role.Troop.Battle.GetEnemies(Role);
}