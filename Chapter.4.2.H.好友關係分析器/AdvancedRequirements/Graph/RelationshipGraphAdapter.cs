using QuickGraph;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Search;

namespace Chapter._4._2.H.好友關係分析器.AdvancedRequirements.Graph;

public class RelationshipGraphAdapter : IRelationshipAnalyzer, IRelationshipGraph
{
    private DepthFirstSearchAlgorithm<string, UndirectedEdge<string>> _dfs;
    public IRelationshipGraph Parse(string script)
    {
        var graph = new AdjacencyGraph<string, UndirectedEdge<string>>();
        var lines = script.Split("\n");
        foreach (var line in lines)
        {
            var parts = line.Split(": ");
            var person = parts[0];
            var friends = parts[1].Split(' ', StringSplitOptions.TrimEntries);
            foreach (var friend in friends)
            {
                graph.AddVerticesAndEdge(new UndirectedEdge<string>(person, friend));
            }
        }
        
        _dfs = new DepthFirstSearchAlgorithm<string, UndirectedEdge<string>>(graph);
        return this;
    }

    public bool HasConnection(string name1, string name2)
    {
        var predecessors = new VertexPredecessorRecorderObserver<string, UndirectedEdge<string>>();
        using (predecessors.Attach(_dfs))
        {
            _dfs.Compute(name1);
        }

        return predecessors.TryGetPath(name2, out _);
    }
}