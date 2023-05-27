using System.Collections;

public abstract class Deck<TCard> : IEnumerable<TCard> where TCard : Card
{
    protected List<TCard> Cards { get;  init; }

    public void Shuffle()
    {
        // Knuth shuffle
        var random = new Random();
        var cardCount = Cards.Count;

        while (cardCount > 1)
        {
            cardCount--;
            var randomIndex = random.Next(cardCount + 1);
            (Cards[randomIndex], Cards[cardCount]) = (Cards[cardCount], Cards[randomIndex]);
        }
    }

    public bool HasCard()
    {
        return Cards.Any();
    }

    public TCard DrawCard()
    {
        if (!HasCard())
        {
            throw new Exception("Cards have been drawn");
        }

        var topCard = Cards[0];
        Cards.RemoveAt(0);
        return topCard;
    }

    public void AddRange(IEnumerable<TCard> cards)
    {
        Cards.AddRange(cards);
    }

    public IEnumerator<TCard> GetEnumerator()
    {
        return Cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}