using System.Collections;
using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.CardPatterns;

public abstract class CardPattern : IEnumerable<Card>
{
    private readonly IEnumerable<Card> Cards;
    public abstract string Name { get; }

    protected CardPattern(IEnumerable<Card> cards)
    {
        if (cards.Count() > 5)
        {
            throw new ArgumentException();
        }

        Cards = cards;
    }


    public override string ToString()
    {
        return string.Join(' ', Cards.Select(card => card.ToString()));
    }

    public IEnumerator<Card> GetEnumerator()
    {
        return Cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}