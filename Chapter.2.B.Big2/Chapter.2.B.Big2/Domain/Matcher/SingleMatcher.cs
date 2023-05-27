using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Matcher;

public class SingleMatcher : CardPatternMatcher
{
    public SingleMatcher(CardPatternMatcher? nextMatcher) : base(nextMatcher)
    {
    }

    protected override CardPattern GetCardPattern(IEnumerable<Card> cards)
    {
        return new SinglePattern(cards);
    }

    protected override bool IsMatch(IEnumerable<Card> cards)
    {
        return cards.Count() == 1;
    }
}