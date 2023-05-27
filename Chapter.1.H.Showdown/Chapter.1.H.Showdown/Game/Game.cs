using Chapter;

public class Game
{
    private const int RoundLimitCount = 13;
    private readonly Deck _deck;
    public readonly List<Player> Players;
    public List<Round> Rounds { get; }

    public Game(List<Player> players)
    {
        Players = players;
        _deck = new Deck();
        Rounds = new List<Round>(RoundLimitCount);
    }

    public void Start()
    {
        PlayersNameHimSelf();
        DeckShuffle();
        PlayersDrawCard();
        RoundStart();
        DisplayFinalWinner();
    }

    private void DeckShuffle()
    {
        _deck.Shuffle();
    }

    private void RoundStart()
    {
        while (Rounds.Count < RoundLimitCount)
        {
            var round = new Round(this);
            Rounds.Add(round);
            round.Start();
        }
    }

    private void DisplayFinalWinner()
    {
        var finalWinner = Players.MaxBy(player => player.Point);
        Console.WriteLine($"""
最後贏家是: {finalWinner.Name} 
獲得 {finalWinner.Point} 點
""");
        Console.ReadLine();
    }

    private void PlayersDrawCard()
    {
        Console.WriteLine("開始輪流抽牌，直到所有人都13張");

        while (_deck.HasCard())
        {
            foreach (var player in Players.TakeWhile(player => player.HandCard.Count < RoundLimitCount))
            {
                player.AddHandCard(_deck.DrawCard());
            }
        }

        Console.WriteLine("抽完牌了");
    }

    private void PlayersNameHimSelf()
    {
        Players.ForEach(player =>
        {
            Console.WriteLine("請輸入自己的名字:");
            player.NameHimSelf();
        });
    }
}