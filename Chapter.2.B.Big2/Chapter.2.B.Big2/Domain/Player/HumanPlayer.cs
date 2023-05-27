using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Player;

public class HumanPlayer : Player
{
    public HumanPlayer(HandCard handCard) : base(handCard)
    {
    }

    public override void NameHimSelf()
    {
        var name = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    public override TurnMove TakeTurn()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var pass) && pass == -1)
            {
                return TurnMove.Pass(this);
            }

            var cardBySplit = input.Split(' ');
            var playCards = new List<Card>();
            foreach (var cardIndex in cardBySplit)
            {
                if (int.TryParse(cardIndex, out var index) is false)
                {
                    Console.WriteLine(cardIndex);
                    Console.WriteLine(index);
                    Console.WriteLine("請輸入數字");
                }
                else if (index < 0 || index > HandCard.Count() - 1)
                {
                    Console.WriteLine($"包含無效的卡牌位置，請輸入 0 ~ {HandCard.Count()}");
                }
                else
                {
                    var card = HandCard[index];
                    playCards.Add(card);
                }
            }

            return TurnMove.Play(this, playCards);
        }
    }
}