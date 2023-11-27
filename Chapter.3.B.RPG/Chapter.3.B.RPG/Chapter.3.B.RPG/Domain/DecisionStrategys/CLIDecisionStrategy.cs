using Action = Chapter._3.B.RPG.Domain.Actions.Action;

namespace Chapter._3.B.RPG.Domain.DecisionStrategies;

public class CLIDecisionStrategy : DecisionStrategy
{
    public override Action ChooseAction()
    {
        while (true)
        {
            ShowActionMenu();
            if (!int.TryParse(Console.ReadLine(), out var actionIndex) &&
                actionIndex < 0 &&
                actionIndex > Role.Actions.Count - 1)
            {
                Console.WriteLine($"請輸入 0 ~ {Role.Actions.Count - 1}");
                continue;
            }

            var action = Role.Actions[actionIndex];

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
        var candidateNames = candidates.Select((role, index) => $"({index}) {role}");
        Console.WriteLine($"選擇 {targetCount} 位目標: {string.Join(' ', candidateNames)}");
        // TODO 驗證 index?
        var readLine = Console.ReadLine();
        var targetIndexes = readLine.Split(", ").Select(int.Parse);
        return targetIndexes.Select(candidates.ElementAt);
    }
}