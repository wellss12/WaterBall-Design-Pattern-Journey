﻿using Chapter._3.B.RPG.Domain.Actions;
using Chapter._3.B.RPG.Domain.DecisionStrategies;
using Chapter._3.B.RPG.Domain.Observers;
using Chapter._3.B.RPG.Domain.States;
using Action = Chapter._3.B.RPG.Domain.Actions.Action;

namespace Chapter._3.B.RPG.Domain.Roles;

public class Role
{
    public Role(string name, int hp, int mp, int str, IEnumerable<Action> actions, DecisionStrategy decisionStrategy)
    {
        decisionStrategy.Role = this;
        _decisionStrategy = decisionStrategy;
        Name = name;
        Hp = hp;
        Mp = mp;
        Str = str;
        State = new NormalState(this);
        _actions.AddRange(actions);
        _actions.ForEach(action => action.Role = this);
    }

    private readonly DecisionStrategy _decisionStrategy;
    private readonly List<IRoleDeadObserver> _roleDeadObservers = new();
    private readonly List<Action> _actions = new() {new BasicAttack()};
    public string Name { get; }
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Str { get; }
    public State State { get; set; }
    public Troop Troop { get; set; }

    public IReadOnlyList<Action> Actions => _actions;

    public IEnumerable<IRoleDeadObserver> RoleDeadObservers => _roleDeadObservers;

    public void StartAction()
    {
        Console.WriteLine($"輪到 {Troop}{Name} (HP: {Hp}, MP: {Mp}, STR: {Str}, {State})。");
        State.ExecuteAction();
        State.EndRoundAction();
    }

    internal void ExecuteAction()
    {
        var action = _decisionStrategy.ChooseAction();
        var targets = ChooseTargets(action);
        action.Execute(targets);
    }

    private IEnumerable<Role> ChooseTargets(Action action)
    {
        var candidates = action.GetCandidates().ToArray();
        if (candidates.Length > action.TargetCount)
        {
            return _decisionStrategy
                .ChooseTargets(candidates, action.TargetCount)
                .ToArray();
        }

        return candidates.Length <= action.TargetCount
            ? candidates
            : Enumerable.Empty<Role>();
    }

    public bool IsDead() => Hp <= 0;

    public bool IsAlive() => !IsDead();

    internal void Damage(Role target, int str) => State.Damage(target, str);

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

    public void Register(IRoleDeadObserver observer) => _roleDeadObservers.Add(observer);

    public override string ToString() => $"[{Troop.Number}]{Name}";
}