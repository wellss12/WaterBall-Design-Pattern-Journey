using Chapter._3.B.RPG.Domain.Actions;
using Chapter._3.B.RPG.Domain.DecisionStrategies;
using Action = Chapter._3.B.RPG.Domain.Actions.Action;

namespace Chapter._3.B.RPG.Domain;

public class Role
{
    public Role(string name, int hp, int mp, int str, List<Action> actions, DecisionStrategy decisionStrategy)
    {
        _decisionStrategy = decisionStrategy;
        Name = name;
        Hp = hp;
        Mp = mp;
        Str = str;
        State = new NormalState();
        actions.Insert(0, new BasicAttack());
        Actions = actions;
        Actions.ForEach(action => action.Role = this);
        decisionStrategy.Role = this;
    }

    private readonly DecisionStrategy _decisionStrategy;
    public string Name { get; }
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Str { get; set; }
    public State State { get; set; }
    public Troop Troop { get; set; }
    public List<Action> Actions { get; }

    public void ExecuteAction()
    {
        Console.WriteLine($"輪到 [{Troop.Number}]{Name} (HP: {Hp}, MP: {Mp}, STR: {Str}, {State})。");
        var action = _decisionStrategy.ChooseAction();

        var enemy = Troop.Battle.GetEnemy(this).Where(enemy => enemy.IsAlive());
        var targets = Enumerable.Empty<Role>();
        
        if (enemy.Count() > action.TargetCount)
        {
            targets = _decisionStrategy.ChooseTargets(enemy, action.TargetCount);
        }
        else if (enemy.Count() <= action.TargetCount)
        {
            targets = enemy;
        }

        action.Execute(targets);
    }

    public override string ToString()
    {
        return $"[{Troop.Number}]{Name}";
    }

    public bool IsDead() => Hp <= 0;

    public bool IsAlive() => !IsDead();

    public void OnDamaged(int str)
    {
        Hp -= str;
        if (Hp <= 0)
        {
            Console.WriteLine($"{this} 死亡。");
        }
    }
}