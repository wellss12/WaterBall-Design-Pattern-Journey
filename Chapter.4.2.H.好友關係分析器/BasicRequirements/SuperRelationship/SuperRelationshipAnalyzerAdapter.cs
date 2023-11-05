namespace Chapter._4._2.H.好友關係分析器.BasicRequirements.SuperRelationship;

public class SuperRelationshipAnalyzerAdapter : IRelationshipAnalyzer
{
    private readonly SuperRelationshipAnalyzer _superRelationshipAnalyzer = new();
    private readonly Dictionary<string, string[]> _relationshipLookup = new();

    public void Parse(string script)
    {
        var splitLine = script.Split("\n");
        var superRelationshipScript = new HashSet<string>();
        foreach (var line in splitLine)
        {
            var (person, friendList) = ParseRelationshipLine(line);
            _relationshipLookup.Add(person, friendList);

            foreach (var friend in friendList)
            {
                var relationship = $"{person} -- {friend}";
                var reversedRelationship = $"{friend} -- {person}";

                if (superRelationshipScript.Contains(reversedRelationship) is false)
                {
                    superRelationshipScript.Add(relationship);
                }
            }
        }

        _superRelationshipAnalyzer.Init(string.Join("\n", superRelationshipScript));
    }

    public IEnumerable<string> GetMutualFriends(string name1, string name2)
    {
        if (_relationshipLookup.TryGetValue(name1, out var name1Friends))
        {
            return name1Friends
                .Where(friend => _superRelationshipAnalyzer.IsMutualFriend(friend, name1, name2))
                .Select(friend => friend);
        }

        return Enumerable.Empty<string>();
    }

    private static (string person, string[] friendList) ParseRelationshipLine(string row)
    {
        var parts = row.Split(": ");
        var person = parts[0];
        var friendList = parts[1].Split(" ", StringSplitOptions.TrimEntries);
        return (person, friendList);
    }
}