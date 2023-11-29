using Chapter._3.B.RPG.Domain.Roles;

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

    public override IEnumerable<Role> GetCandidates() => Role.Troop.Battle.GetEnemies(Role);
}