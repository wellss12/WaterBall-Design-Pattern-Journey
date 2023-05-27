using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Matcher;

public class StraightMatcher : CardPatternMatcher
{
    public StraightMatcher(CardPatternMatcher? nextMatcher) : base(nextMatcher)
    {
    }

    protected override bool IsMatch(IEnumerable<Card> cards)
    {
        var straightPatterns = new[] {"345678910JQKA2", "3QKA2", "34KA2", "345A2", "34562"};
        var cardTexts = GetCardTexts(cards);
        return straightPatterns.Any(t => t.Contains(cardTexts)) && cards.Count() == 5;
    }

    private static string GetCardTexts(IEnumerable<Card> cards) =>
        string.Join("", cards.OrderBy(card => card.Rank).Select(card => card.RankLookup[card.Rank]));

    protected override CardPattern GetCardPattern(IEnumerable<Card> cards) =>
        new StraightPattern(cards.OrderBy(card => card.Rank));
}