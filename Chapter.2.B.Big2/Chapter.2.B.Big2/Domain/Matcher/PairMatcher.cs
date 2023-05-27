using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Matcher;

public class PairMatcher : CardPatternMatcher
{
    public PairMatcher(CardPatternMatcher? nextMatcher) : base(nextMatcher)
    {
    }

    protected override CardPattern GetCardPattern(IEnumerable<Card> cards)
    {
        return new PairPattern(cards);
    }

    protected override bool IsMatch(IEnumerable<Card> cards)
    {
        var groupBy = cards
            .GroupBy(card => card.Rank)
            .ToList();

        return groupBy.Any(grouping => grouping.Count() == 2) && groupBy.Count == 1;
    }
}