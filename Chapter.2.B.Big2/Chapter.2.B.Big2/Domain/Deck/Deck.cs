namespace Chapter._2.B.Big2.Domain.Deck;

public class Deck
{
    private readonly List<Card> _cards;

    public Deck(List<Card> cards)
    {
        _cards = cards;
    }

    public Card Deal()
    {
        if (HasCards() is false)
        {
            throw new Exception("cards empty");
        }

        var card = _cards.Last();
        _cards.Remove(card);
        return card;
    }

    public bool HasCards()
    {
        return _cards.Any();
    }
}