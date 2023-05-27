namespace Chapter;

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

    protected override Card ShowCard()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var index) is false)
            {
                Console.WriteLine("請輸入數字");
            }
            else if (index < 1 || index > HandCard.Count)
            {
                Console.WriteLine($"請輸入 1 ~ {HandCard.Count}");
            }
            else
            {
                var card = HandCard.Cards[index - 1];
                HandCard.Cards.Remove(card);
                return card;
            }
        }
    }

    protected override Player DecideExchangePlayer()
    {
        while (true)
        {
            Console.WriteLine($"{Name} 要跟哪一位玩家交換? 請輸入玩家名稱");

            var playerName = Console.ReadLine();
            var player = Game.Players.FirstOrDefault(player => player.Name == playerName);

            if (player is null)
            {
                Console.WriteLine("無此玩家");
            }
            else if (player.Name.Equals(Name))
            {
                Console.WriteLine("不能跟自己交換");
            }
            else if (HandCard.Count != player.HandCard.Count)
            {
                Console.WriteLine("雙方玩家手牌數量不一致，請找其他人交換");
            }
            else
            {
                return player;
            }
        }
    }


    protected override bool IsExchangeHands()
    {
        while (true)
        {
            Console.WriteLine($"{Name} 要不要跟其他人交換手排?  回答 y/n");
            var answer = Console.ReadLine();

            switch (answer?.ToLower())
            {
                case "y":
                    return true;
                case "n":
                    return false;
                default:
                    Console.WriteLine("輸入錯誤，請重新輸入");
                    break;
            }
        }
    }
}