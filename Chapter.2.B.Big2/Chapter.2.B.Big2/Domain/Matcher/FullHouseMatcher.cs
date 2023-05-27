using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Matcher;

public class FullHouseMatcher : CardPatternMatcher
{
    public FullHouseMatcher(CardPatternMatcher? nextMatcher) : base(nextMatcher)
    {
    }

    protected override bool IsMatch(IEnumerable<Card> cards)
    {
        var groupBy = cards
            .GroupBy(card => card.Rank)
            .ToList();

        var isPair = groupBy.Any(grouping => grouping.Count() == 2);
        var isThreeOfAKind = groupBy.Any(grouping => grouping.Count() == 3);

        return isPair && isThreeOfAKind && groupBy.Count == 2;
    }

    protected override CardPattern GetCardPattern(IEnumerable<Card> cards)
    {
        return new FullHousePattern(cards.OrderBy(card => card.Rank).ThenBy(card => card.Suit));
    }
}