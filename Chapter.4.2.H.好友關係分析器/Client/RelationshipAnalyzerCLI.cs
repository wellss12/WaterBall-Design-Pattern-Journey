namespace Chapter._4._2.H.好友關係分析器.Client;

public class RelationshipAnalyzerCLI
{
    private readonly BasicRequirements.IRelationshipAnalyzer _superRelationshipAdapter;
    private readonly AdvancedRequirements.IRelationshipAnalyzer _relationshipGraphAdapter;

    public RelationshipAnalyzerCLI(
        BasicRequirements.IRelationshipAnalyzer superRelationshipAdapter,
        AdvancedRequirements.IRelationshipAnalyzer relationshipGraphAdapter)
    {
        _superRelationshipAdapter = superRelationshipAdapter;
        _relationshipGraphAdapter = relationshipGraphAdapter;
    }

    public void StartBasicRequirements()
    {
        var script = File.ReadAllText("script.txt");
        _superRelationshipAdapter.Parse(script);
        Console.WriteLine("以解析完所有好友清單！");
        while (true)
        {
            Console.WriteLine("請輸入兩個名字，分析器會告訴你共同好友有哪些人。");
            Console.Write("請輸入第一個名字：");
            var person1 = Console.In.ReadLine();
            Console.Write("請輸入第二個名字：");
            var person2 = Console.In.ReadLine();
            if (!string.IsNullOrWhiteSpace(person1) && !string.IsNullOrWhiteSpace(person2))
            {
                var mutualFriends = _superRelationshipAdapter.GetMutualFriends(person1, person2);
                Console.WriteLine($"共同好友有：{string.Join(",", mutualFriends)}");
            }
        }
    }

    public void StartAdvancedRequirements()
    {
        var script = File.ReadAllText("script.txt");
        var relationshipGraph = _relationshipGraphAdapter.Parse(script);
        Console.WriteLine(relationshipGraph.HasConnection("A", "B"));
        Console.WriteLine(relationshipGraph.HasConnection("A", "Z"));
        Console.WriteLine(relationshipGraph.HasConnection("B", "F"));
    }
}