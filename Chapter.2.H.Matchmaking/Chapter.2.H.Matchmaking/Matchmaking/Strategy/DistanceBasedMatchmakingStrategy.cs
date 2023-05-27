public class DistanceBasedMatchmakingStrategy : IMatchmakingStrategy
{
    public IEnumerable<Individual> SortByMatch(Individual matcher, IEnumerable<Individual> matchees)
    {
        return matchees
            .OrderBy(matchee => matcher.Coord.GetDistance(matchee.Coord))
            .ThenBy(matchee => matchee.Id);
    }
}