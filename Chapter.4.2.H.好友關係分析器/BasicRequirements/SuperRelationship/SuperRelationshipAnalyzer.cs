namespace Chapter._4._2.H.好友關係分析器.BasicRequirements.SuperRelationship;

public class SuperRelationshipAnalyzer
{
    private readonly Dictionary<string, List<string>> _relationshipLookup = new();

    public void Init(string script)
    {
        var relationshipTuple = script
            .Split("\n")
            .Select(line =>
            {
                var parts = line.Split(" -- ");
                var person = parts[0];
                var friend = parts[1];
                return (person, friend);
            });
        
        foreach (var (person, friend) in relationshipTuple)
        {
            _relationshipLookup.TryAdd(person, new List<string>());
            _relationshipLookup.TryAdd(friend, new List<string>());
            _relationshipLookup[person].Add(friend);
            _relationshipLookup[friend].Add(person);
        }
    }

    public bool IsMutualFriend(string targetName, string name1, string name2)
    {
        var name1Exists = _relationshipLookup.TryGetValue(name1, out var name1Friends);
        var name2Exists = _relationshipLookup.TryGetValue(name2, out var name2Friends);
        if (name1Exists && name2Exists)
        {
            var mutualFriends = name1Friends!.Intersect(name2Friends!);
            return mutualFriends.Contains(targetName);
        }

        return false;
    }
}