namespace Chapter._4._2.H.好友關係分析器.BasicRequirements;

public interface IRelationshipAnalyzer
{
    void Parse(string script);
    IEnumerable<string> GetMutualFriends(string name1, string name2);
}