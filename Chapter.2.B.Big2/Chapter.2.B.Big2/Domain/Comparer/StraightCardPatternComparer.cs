using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Comparer;

public class StraightCardPatternComparer : SameCardPatternComparer
{
    protected override Card GetMaxCard(CardPattern cardPattern)
    {
        return cardPattern.OrderByDescending(t => t.Rank).First();
    }
}