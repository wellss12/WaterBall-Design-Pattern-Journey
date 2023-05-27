namespace Chapter._2.B.Big2.Domain.Deck;

public class Card
{
    public readonly Dictionary<Rank, string> RankLookup = new()
    {
        {Rank.Two, "2"},
        {Rank.Three, "3"},
        {Rank.Four, "4"},
        {Rank.Five, "5"},
        {Rank.Six, "6"},
        {Rank.Seven, "7"},
        {Rank.Eight, "8"},
        {Rank.Nine, "9"},
        {Rank.Ten, "10"},
        {Rank.Jack, "J"},
        {Rank.Queen, "Q"},
        {Rank.King, "K"},
        {Rank.Ace, "A"}
    };

    public readonly Dictionary<Suit, string> SuitLookup = new()
    {
        {Suit.Club, "C"},
        {Suit.Diamond, "D"},
        {Suit.Heart, "H"},
        {Suit.Spade, "S"}
    };

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public Suit Suit { get; }
    public Rank Rank { get; }

    public override string ToString()
    {
        return $"{SuitLookup[Suit]}[{RankLookup[Rank]}]";
    }

    public int Compare(Card second)
    {
        return Rank == second.Rank
            ? Suit - second.Suit
            : Rank - second.Rank;
    }
}