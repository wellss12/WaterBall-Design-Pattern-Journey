using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Player;

public class TurnMove
{
    private TurnMove(Player player, IEnumerable<Card>? cards, bool isPass)
    {
        Player = player;
        IsPass = isPass;
        Cards = cards;
    }

    private Player Player { get; }
    public bool IsPass { get; }
    public IEnumerable<Card>? Cards { get; }

    public static TurnMove Pass(Player player)
    {
        return new TurnMove(player, null, true);
    }

    public static TurnMove Play(Player player, IEnumerable<Card> cards)
    {
        foreach (var card in cards)
        {
            player.HandCard.Remove(card);
        }

        return new TurnMove(player, cards, false);
    }

    public void Undo()
    {
        foreach (var card in Cards!)
        {
            Player.AddHandCard(card);
        }
    }
}