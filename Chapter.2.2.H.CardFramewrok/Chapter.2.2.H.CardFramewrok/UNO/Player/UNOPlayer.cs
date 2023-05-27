using System.Collections.Immutable;

public abstract class UNOPlayer : Player<UNOCard>
{
    protected UNOPlayer() : base(new HandCard<UNOCard>(5))
    {
    }

    public UNO Game { get; set; }

    public void TakeTurn()
    {
        if (GetShowableCards().Any() is false)
        {
            if (Game.Deck.HasCard() is false)
            {
                Game.PutCardsToDeck();
            }

            AddHandCard(Game.Deck.DrawCard());
            Console.WriteLine($"{Name} 沒牌出，只能抽牌了");
        }
        else
        {
            Console.WriteLine($"{Name} 的手牌有 {HandCard.AllCards()}");
            Console.WriteLine($"{Name} 要出第幾張牌?");

            var card = ShowCard();
            Game.OnTableLatestCard = card;
            Game.OnTableCards.Add(card);

            Console.WriteLine($"場面上的牌為:{Game.OnTableLatestCard}");
        }
    }

    protected ImmutableList<UNOCard> GetShowableCards()
    {
        return HandCard
            .Where(card => card.CanShowCard(Game.OnTableLatestCard))
            .ToImmutableList();
    }
}