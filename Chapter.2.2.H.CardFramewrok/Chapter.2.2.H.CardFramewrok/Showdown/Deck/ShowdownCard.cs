public class ShowdownCard : Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public ShowdownCard(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Suit.ToString()}-{Rank.ToString()}";
    }

    public int Compare(ShowdownCard card)
    {
        return Rank == card.Rank
            ? Suit - card.Suit
            : Rank - card.Rank;
    }
}