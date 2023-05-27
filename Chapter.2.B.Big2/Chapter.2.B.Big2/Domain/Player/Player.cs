using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Player;

public abstract class Player
{
    public HandCard HandCard { get; }
    public string Name { get; protected set; }

    protected Player(HandCard handCard)
    {
        HandCard = handCard;
    }

    public abstract void NameHimSelf();

    public abstract TurnMove TakeTurn();

    public void AddHandCard(Card card)
    {
        if (HandCard.Count() >= 13)
        {
            throw new Exception("Hands can only have 13 cards");
        }

        HandCard.Add(card);
    }

    public bool HasHandCard()
    {
        return HandCard.Any();
    }
}