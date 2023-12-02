using Chapter._3.B.RPG.Domain.Roles;
using Action = Chapter._3.B.RPG.Domain.Actions.Action;

namespace Chapter._3.B.RPG.Domain.DecisionStrategies;

public class AIDecisionStrategy : DecisionStrategy
{
    private int _seed;

    public override Action ChooseAction()
    {
        while (true)
        {
            if (Role.Name == "Ally")
            {
                
            }
            ShowActionMenu();
            var actionIndex = _seed % Role.Actions.Count;
            var action = Role.Actions[actionIndex];
            _seed++;
            if (action.MpCost > Role.Mp)
            {
                Console.WriteLine("你缺乏 MP，不能進行此行動。");
                continue;
            }

            return action;
        }
    }

    public override IEnumerable<Role> ChooseTargets(IEnumerable<Role> candidates, int targetCount)
    {
        if (Role.Name == "Ally")
        {
                
        }

        for (var index = 0; index < targetCount; index++)
        {
            var candidateIndex = (_seed + index) % candidates.Count();
            yield return candidates.ElementAt(candidateIndex);
        }

        _seed++;
    }
}