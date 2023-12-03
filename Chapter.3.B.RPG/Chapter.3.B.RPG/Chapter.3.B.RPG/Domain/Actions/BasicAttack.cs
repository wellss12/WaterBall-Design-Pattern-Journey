using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Actions;

public class BasicAttack : Action
{
    public override string Name => "普通攻擊";
    public override int TargetCount => 1;
    public override int Str => Role.Str;

    public override void Execute(IEnumerable<Role> targets)
    {
        foreach (var target in targets)
        {
            Console.WriteLine($"{Role} 攻擊 {target}。");
            Role.Damage(target, Str);
        }
    }

    public override IEnumerable<Role> GetCandidates() => Role.Troop.GetAliveEnemies();
}