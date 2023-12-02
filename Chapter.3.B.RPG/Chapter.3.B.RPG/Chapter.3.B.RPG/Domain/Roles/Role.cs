using Chapter._3.B.RPG.Domain.Actions;
using Chapter._3.B.RPG.Domain.DecisionStrategies;
using Chapter._3.B.RPG.Domain.Observers;
using Chapter._3.B.RPG.Domain.States;
using Action = Chapter._3.B.RPG.Domain.Actions.Action;

namespace Chapter._3.B.RPG.Domain.Roles;

public class Role
{
    public Role(string name, int hp, int mp, int str, List<Action> actions, DecisionStrategy decisionStrategy)
    {
        _decisionStrategy = decisionStrategy;
        Name = name;
        Hp = hp;
        Mp = mp;
        Str = str;
        State = new NormalState(this);
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
    public List<IRoleDeadObserver> RoleDeadObservers { get; } = new();

    public void StartAction()
    {
        Console.WriteLine($"輪到 [{Troop.Number}]{Name} (HP: {Hp}, MP: {Mp}, STR: {Str}, {State})。");
        State.ExecuteAction();
        State.EndRoundAction();
    }

    public void ExecuteAction()
    {
        var action = _decisionStrategy.ChooseAction();

        var candidates = action.GetCandidates().ToList();
        var targets = Enumerable.Empty<Role>();

        if (candidates.Count > action.TargetCount)
        {
            targets = _decisionStrategy
                .ChooseTargets(candidates, action.TargetCount)
                .ToList();
        }
        else if (candidates.Count <= action.TargetCount)
        {
            targets = candidates;
        }

        action.Execute(targets);
    }

    public override string ToString()
    {
        return $"[{Troop.Number}]{Name}";
    }

    public bool IsDead() => Hp <= 0;

    public bool IsAlive() => !IsDead();

    public void Damage(Role target, int str) => State.Damage(target, str);

    public void OnDamaged(int str)
    {
        Hp -= str;
        if (IsDead())
        {
            foreach (var observer in RoleDeadObservers)
            {
                observer.Update(this);
            }

            Console.WriteLine($"{this} 死亡。");
        }
    }

    public void Register(IRoleDeadObserver observer) => RoleDeadObservers.Add(observer);
}