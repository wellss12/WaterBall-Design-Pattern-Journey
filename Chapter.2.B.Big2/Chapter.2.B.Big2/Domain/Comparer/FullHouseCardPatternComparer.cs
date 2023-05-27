using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Comparer;

public class FullHouseCardPatternComparer : SameCardPatternComparer
{
    protected override Card GetMaxCard(CardPattern cardPattern)
    {
        return GetThreeOfAKind(cardPattern).First();
    }

    private static IEnumerable<Card> GetThreeOfAKind(CardPattern cardPattern)
    {
        return cardPattern
            .GroupBy(card => card.Rank)
            .First(grouping => grouping.Count() == 3);
    }
}