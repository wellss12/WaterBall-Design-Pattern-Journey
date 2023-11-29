using Chapter._3.B.RPG.Domain.DecisionStrategies;
using Action = Chapter._3.B.RPG.Domain.Actions.Action;

namespace Chapter._3.B.RPG.Domain.Roles;

public class Hero : Role
{
    public Hero(string name, int hp, int mp, int str, List<Action> actions, DecisionStrategy decisionStrategy)
        : base(name, hp, mp, str, actions, decisionStrategy)
    {
    }
}