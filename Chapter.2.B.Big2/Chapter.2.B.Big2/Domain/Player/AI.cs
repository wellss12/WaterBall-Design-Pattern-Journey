using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Player;

public class AI : Player
{
    private readonly Random _random;

    public AI(HandCard handCard) : base(handCard)
    {
        _random = new Random();
    }

    public override void NameHimSelf()
    {
        var number = GetRandomNumber();
        var name = $"{nameof(AI)}-{number}";
        Name = name;
        Console.WriteLine(name);
    }

    public override TurnMove TakeTurn()
    {
        while (true)
        {
            var input = GetRandomNumber(-1, 6);
            if (input == -1)
            {
                return TurnMove.Pass(this);
            }
            
            var selectedIndexes = new HashSet<int>();
            var playCards = new List<Card>();
            for (var i = 1; i <= input; i++)
            {
                int index;
                do
                {
                    index = GetRandomNumber(0, HandCard.Count());
                } while (selectedIndexes.Contains(index)); 

                selectedIndexes.Add(index); 

                var card = HandCard[index];
                playCards.Add(card);
            }

            return TurnMove.Play(this, playCards);
        }
    }

    private int GetRandomNumber(int minValue = 0, int maxValue = 1000)
    {
        return _random.Next(minValue, maxValue);
    }
}