using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.CardPatterns;

public class SinglePattern : CardPattern
{
    public SinglePattern(IEnumerable<Card> cards) : base(cards)
    {
    }

    public override string Name => "單張";
}