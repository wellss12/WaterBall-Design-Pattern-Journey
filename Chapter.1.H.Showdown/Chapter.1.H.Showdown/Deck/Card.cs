public class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Suit.ToString()}-{Rank.ToString()}";
    }

    public int Compare(Card card)
    {
        return Rank == card.Rank
            ? Suit - card.Suit
            : Rank - card.Rank;
    }
}