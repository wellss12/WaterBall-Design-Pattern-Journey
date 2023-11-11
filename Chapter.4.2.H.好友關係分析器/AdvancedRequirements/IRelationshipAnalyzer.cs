using Chapter._4._2.H.好友關係分析器.AdvancedRequirements.Graph;

namespace Chapter._4._2.H.好友關係分析器.AdvancedRequirements;

public interface IRelationshipAnalyzer
{
    IRelationshipGraph Parse(string script);
}