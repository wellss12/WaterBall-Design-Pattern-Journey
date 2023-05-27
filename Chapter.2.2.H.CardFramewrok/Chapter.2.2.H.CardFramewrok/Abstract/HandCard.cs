using System.Collections;

public class HandCard<TCard> : IEnumerable<TCard> where TCard : Card
{
    private List<TCard> Cards { get; }

    public HandCard(int cardCount)
    {
        Cards = new List<TCard>(cardCount);
    }

    public string AllCards()
    {
        return string.Join(", ", Cards.Select(card => card!.ToString()));
    }

    public void Remove(TCard card)
    {
        Cards.Remove(card);
    }

    public void Add(TCard card)
    {
        Cards.Add(card);
    }

    public int IndexOf(TCard card)
    {
        return Cards.IndexOf(card);
    }

    public TCard this[int index] => Cards[index];

    public IEnumerator<TCard> GetEnumerator()
    {
        return Cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}