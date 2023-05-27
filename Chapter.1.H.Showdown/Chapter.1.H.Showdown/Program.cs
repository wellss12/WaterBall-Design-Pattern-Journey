using Chapter;

var players = new List<Player>
{
    new HumanPlayer(new HandCard()),
    new HumanPlayer(new HandCard()),
    new HumanPlayer(new HandCard()),
    new AI(new HandCard())
};

var game = new Game(players);
players.ForEach(player => { player.Game = game; });

game.Start();