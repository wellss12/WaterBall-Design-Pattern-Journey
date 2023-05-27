

StartShowdown();
StartUNO();

void StartShowdown()
{
    var showdownPlayers = new List<ShowdownPlayer>
    {
        new ShowdownHumanPlayer(),
        new ShowdownHumanPlayer(),
        new ShowdownHumanPlayer(),
        new ShowdownAI()
    };
    var showdown = new Showdown(showdownPlayers);
    showdownPlayers.ForEach(player => player.Game = showdown);
    showdown.Start();
}

void StartUNO()
{
    var unoPlayers = new List<UNOPlayer>
    {
        new UNOAI(),
        new UNOHumanPlayer(),
        new UNOAI(),
        new UNOHumanPlayer(),
    };

    var uno = new UNO(unoPlayers);
    unoPlayers.ForEach(player => player.Game = uno);

    uno.Start();
}