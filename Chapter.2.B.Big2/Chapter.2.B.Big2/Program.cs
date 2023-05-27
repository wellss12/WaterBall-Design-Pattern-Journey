using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Comparer;
using Chapter._2.B.Big2.Domain.Deck;
using Chapter._2.B.Big2.Domain.Game;
using Chapter._2.B.Big2.Domain.Matcher;
using Chapter._2.B.Big2.Domain.Player;

public class Program
{
    public static void Main()
    {
        // Console.WriteLine 預設會產生 \r\n 
        // 但 測資都是 \n，所以加上這個全域設定
        // 讓 Console.WriteLine 預設產生 \n
        Console.Out.NewLine = "\n";

        var players = new List<Player>
        {
            new HumanPlayer(new HandCard()),
            new HumanPlayer(new HandCard()),
            new HumanPlayer(new HandCard()),
            new HumanPlayer(new HandCard())
        };

        var deck = GetDeck();
        var matcher = new SingleMatcher(new PairMatcher(new StraightMatcher(new FullHouseMatcher(null))));
        var comparersLookup = new Dictionary<Type, SameCardPatternComparer>
        {
            {typeof(SinglePattern), new SingleCardPatternComparer()},
            {typeof(PairPattern), new PairCardPatternComparer()},
            {typeof(StraightPattern), new StraightCardPatternComparer()},
            {typeof(FullHousePattern), new FullHouseCardPatternComparer()},
        };

        var game = new Big2(players, deck, matcher, comparersLookup);
        game.Start();
    }

    private static Deck GetDeck()
    {
        var rankLookup = new Dictionary<string, Rank>
        {
            {"2", Rank.Two},
            {"3", Rank.Three},
            {"4", Rank.Four},
            {"5", Rank.Five},
            {"6", Rank.Six},
            {"7", Rank.Seven},
            {"8", Rank.Eight},
            {"9", Rank.Nine},
            {"10", Rank.Ten},
            {"J", Rank.Jack},
            {"Q", Rank.Queen},
            {"K", Rank.King},
            {"A", Rank.Ace}
        };

        var suitLookup = new Dictionary<string, Suit>
        {
            {"C", Suit.Club},
            {"D", Suit.Diamond},
            {"H", Suit.Heart},
            {"S", Suit.Spade}
        };

        var deckString = Console.ReadLine();

        var cards = new List<Card>(52);
        foreach (var card in deckString.Split(' '))
        {
            var (suitInput, rankInput) = ParseCard(card);
            if (suitLookup.TryGetValue(suitInput, out var suit) && rankLookup.TryGetValue(rankInput, out var rank))
            {
                cards.Add(new Card(suit, rank));
            }
        }

        return new Deck(cards);
    }

    private static (string suitInput, string rankInput) ParseCard(string card)
    {
        var startIndex = card.IndexOf("[", StringComparison.Ordinal) + 1;
        var endIndex = card.IndexOf("]", StringComparison.Ordinal);
        var rankInput = card.Substring(startIndex, endIndex - startIndex);

        return (card[0].ToString(), rankInput);
    }
}