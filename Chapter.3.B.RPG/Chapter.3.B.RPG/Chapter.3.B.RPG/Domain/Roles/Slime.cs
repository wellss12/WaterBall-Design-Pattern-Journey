using Chapter._3.B.RPG.Domain.DecisionStrategies;
using Action = Chapter._3.B.RPG.Domain.Actions.Action;

namespace Chapter._3.B.RPG.Domain.Roles;

public class Slime : Role
{
    public Slime() : base("Slime", 100, 0, 50, new List<Action>(), new AIDecisionStrategy())
    {
    }
}