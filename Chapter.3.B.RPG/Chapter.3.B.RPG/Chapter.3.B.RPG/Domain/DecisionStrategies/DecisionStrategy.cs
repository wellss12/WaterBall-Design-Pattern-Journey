using Action = Chapter._3.B.RPG.Domain.Actions.Action;

namespace Chapter._3.B.RPG.Domain.DecisionStrategies;

public abstract class DecisionStrategy
{
    public Role Role { get; set; }
    public abstract Action ChooseAction();
    public abstract IEnumerable<Role> ChooseTargets(IEnumerable<Role> candidates, int targetCount);
    
    protected void ShowActionMenu()
    {
        var actionNames = Role.Actions.Select((action, index) => $"({index}) {action.Name}");
        var allActionNames = string.Join(' ', actionNames);
        Console.WriteLine($"選擇行動：{allActionNames}");
    }
}