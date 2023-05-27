public class ReverseDecorator : IMatchmakingStrategy
{
    private readonly IMatchmakingStrategy _matchmakingStrategy;

    public ReverseDecorator(IMatchmakingStrategy matchmakingStrategy)
    {
        _matchmakingStrategy = matchmakingStrategy;
    }

    public IEnumerable<Individual> SortByMatch(Individual matcher, IEnumerable<Individual> matchees)
    {
        return _matchmakingStrategy
            .SortByMatch(matcher, matchees)
            .Reverse();    
    }
}