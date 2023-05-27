public abstract class Player<TCard> where TCard : Card
{
    public HandCard<TCard> HandCard { get; }
    public string Name { get; protected set; }

    protected Player(HandCard<TCard> handCard)
    {
        HandCard = handCard;
    }

    public abstract void NameHimSelf();

    protected abstract TCard ShowCard();

    public void AddHandCard(TCard card)
    {
        HandCard.Add(card);
    }
}