using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.CardPatterns;

public class FullHousePattern : CardPattern
{
    public FullHousePattern(IEnumerable<Card> cards) : base(cards)
    {
    }

    public override string Name => "葫蘆";
}