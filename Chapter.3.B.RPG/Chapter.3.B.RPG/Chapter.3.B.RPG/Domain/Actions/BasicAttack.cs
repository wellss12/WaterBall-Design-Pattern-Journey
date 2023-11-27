namespace Chapter._3.B.RPG.Domain.Actions;

public class BasicAttack : Action
{
    public override string Name => "普通攻擊";
    public override int TargetCount => 1;
    public override int MpCost => 0;
    public override int Str => Role.Str;

    public override void Execute(IEnumerable<Role> targets)
    {
        foreach (var target in targets)
        {
            Console.WriteLine($"{Role} 攻擊 {target}。");
            Console.WriteLine($"{Role} 對 {target} 造成 {Role.Str} 點傷害。");
            target.OnDamaged(Role.Str);
        }

        Role.Mp -= MpCost;
    }
}

public abstract class Skill : Action
{
}

public class Waterball : Skill
{
    public override string Name => "水球";
    public override int TargetCount => 1;
    public override int MpCost => 50;
    public override int Str => 120;

    public override void Execute(IEnumerable<Role> targets)
    {
        var targetNames = targets.Select(target => target.ToString());
        var targetNameString = string.Join(", ", targetNames);
        Console.WriteLine($"{Role} 對 {targetNameString} 使用了 {Name}。");

        foreach (var target in targets)
        {
            Console.WriteLine($"{Role} 對 {target} 造成 {Str} 點傷害。");
            target.OnDamaged(Str);
        }

        Role.Mp -= MpCost;
    }
}

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
            Console.WriteLine($"{Role} 對 {target} 造成 {Str} 點傷害。");
            target.OnDamaged(Str);
        }

        Role.Mp -= MpCost;
    }
}