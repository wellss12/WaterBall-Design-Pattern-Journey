using System.Collections.Immutable;

public class MatchmakingSystem
{
    private readonly IReadOnlyCollection<Individual> _individuals;
    private readonly IMatchmakingStrategy _matchmakingStrategy;

    public MatchmakingSystem(IEnumerable<Individual> individuals, IMatchmakingStrategy matchmakingStrategy)
    {
        _individuals = individuals.ToImmutableList();
        _matchmakingStrategy = matchmakingStrategy;
    }
    public void Match()
    {
        foreach (var matcher in _individuals)
        {
            var matchees = _individuals.Where(t => t.Id != matcher.Id);
            var matchedOrder = _matchmakingStrategy.SortByMatch(matcher, matchees);
            matcher.Match(matchedOrder.First());
        }
    }
}