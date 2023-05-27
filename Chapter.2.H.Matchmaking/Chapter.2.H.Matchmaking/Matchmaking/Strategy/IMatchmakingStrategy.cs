public interface IMatchmakingStrategy
{
    public IEnumerable<Individual> SortByMatch(Individual matcher, IEnumerable<Individual> matchees);
}