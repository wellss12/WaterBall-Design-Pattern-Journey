using System.Collections;
using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Player;

public class HandCard : IEnumerable<Card>
{
    private List<Card> Cards { get; }

    public HandCard()
    {
        Cards = new List<Card>(13);
    }

    public void Add(Card card)
    {
        var index = Cards.FindIndex(c => c.Rank > card.Rank || (c.Rank == card.Rank && c.Suit > card.Suit));

        if (index == -1)
        {
            Cards.Add(card);
        }
        else
        {
            Cards.Insert(index, card);
        }
    }

    public void Remove(Card card)
    {
        Cards.Remove(card);
    }

    public bool HasClub3()
    {
        return Cards.Any(card => card is {Suit: Suit.Club, Rank: Rank.Three});
    }

    public Card this[int index] => Cards[index];

    public void ShowAll()
    {
        // Format 格式 (PreviousCard.length - PreviousCardIndex.length) + space.Length + CurrentCardIndex.Length
        const int space = 1;
        var spaceLengthLookup = Cards
            .Select((card, index) => new {index, card})
            .ToDictionary(t => t.index, t =>
            {
                var cardLength = t.card.ToString().Length;
                var indexLength = t.index.ToString().Length;
                return cardLength - indexLength + space;
            });

        Console.Write("0");
        for (var i = 1; i < Cards.Count; i++)
        {
            var currentIndexLength = i.ToString().Length;
            var previousSpaceLength = spaceLengthLookup[i - 1];
            Console.Write(i.ToString().PadLeft(previousSpaceLength + currentIndexLength));
        }

        Console.WriteLine();
        Console.Write(string.Join(' ', Cards));
        Console.WriteLine();
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