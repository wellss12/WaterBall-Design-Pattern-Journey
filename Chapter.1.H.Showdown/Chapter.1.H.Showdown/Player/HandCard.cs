public class HandCard
{
    public int Count => Cards.Count;
    public List<Card> Cards { get; }

    public HandCard()
    {
        Cards = new List<Card>(13);
    }

    public string AllCards()
    {
        return string.Join(", ", Cards.Select(card => card.ToString()));
    }
}