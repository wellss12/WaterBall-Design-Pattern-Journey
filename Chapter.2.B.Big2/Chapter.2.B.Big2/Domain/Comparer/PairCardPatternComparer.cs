using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Comparer;

public class PairCardPatternComparer : SameCardPatternComparer
{
    protected override Card GetMaxCard(CardPattern cardPattern)
    {
        return GetPair(cardPattern).First();
    }

    private static IOrderedEnumerable<Card> GetPair(CardPattern cardPattern)
    {
        return cardPattern
            .GroupBy(card => card.Rank)
            .First(grouping => grouping.Count() == 2)
            .OrderByDescending(card => card.Suit);
    }
}