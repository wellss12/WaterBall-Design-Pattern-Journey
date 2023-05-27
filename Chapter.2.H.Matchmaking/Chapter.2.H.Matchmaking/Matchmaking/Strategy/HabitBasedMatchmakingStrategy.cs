public class HabitBasedMatchmakingStrategy : IMatchmakingStrategy
{
    public IEnumerable<Individual> SortByMatch(Individual matcher, IEnumerable<Individual> matchees)
    {
        return matchees
            .OrderByDescending(matchee => matcher.GetCommonInterests(matchee.Habits).Count())
            .ThenBy(matchee => matchee.Id);
    }
}