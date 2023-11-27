using Action = Chapter._3.B.RPG.Domain.Actions.Action;

namespace Chapter._3.B.RPG.Domain.DecisionStrategies;

public class AIDecisionStrategy : DecisionStrategy
{
    private int _seed;

    public override Action ChooseAction()
    {
        ShowActionMenu();
        // TODO: 驗證是否有足夠的 MP 使用技能
        var actionIndex = _seed % Role.Actions.Count;
        _seed++;
        return Role.Actions[actionIndex];
    }

    public override IEnumerable<Role> ChooseTargets(IEnumerable<Role> candidates, int targetCount)
    {
        for (var index = 0; index < targetCount; index++)
        {
            var candidateIndex = (_seed + index) % candidates.Count();
            yield return candidates.ElementAt(candidateIndex);
        }

        _seed++;
    }
}