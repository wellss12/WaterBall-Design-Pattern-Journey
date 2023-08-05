namespace Chapter._3._3.H.寶藏地圖.Domain.Maps;

public class MapAdventureGame
{
    private readonly Map _map;

    public MapAdventureGame()
    {
        _map = new Map();
    }

    public void Start()
    {
        while (IsGameOver() is false)
        {
            RoundStart();
            if (IsGameOver())
            {
                _map.DisplayWinner();
            }
        }
    }

    private void RoundStart()
    {
        Console.WriteLine("-----------回合開始-----------");

        _map.DisplayMapStatus();
        var character = _map.GetCharacter();
        character.RoundStart();

        foreach (var monster in _map.GetMonsters())
        {
            monster.RoundStart();
        }

        Console.WriteLine("-----------回合結束-----------");
    }

    private bool IsGameOver()
    {
        return _map.AllMonstersDead() && _map.IsCharacterDead();
    }
}