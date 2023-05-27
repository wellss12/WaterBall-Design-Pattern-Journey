using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.CardPatterns;

public class StraightPattern : CardPattern
{
    public StraightPattern(IEnumerable<Card> cards) : base(cards)
    {
    }

    public override string Name => "順子";
}