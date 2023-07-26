using Chapter._3._3.H.寶藏地圖.Domain.MapObject.Role;

namespace Chapter._3._3.H.寶藏地圖.Domain.Map;

public class MapAdventureGame
{
    private readonly Map _map;

    public MapAdventureGame()
    {
        _map = new Map();
    }

    public void Start()
    {
        while (IsGameOver())
        {
            Console.WriteLine("-----------回合開始-----------");

            Console.WriteLine($"Map Size: {_map.MapObjects.GetLength(0)} X {_map.MapObjects.GetLength(1)}");
            var character = _map.GetCharacter();
            var characterPosition = character.Position;
            Console.WriteLine(
                $"Hp: {character.Hp}, State: {character.State}, Position: [{characterPosition.Row},{characterPosition.Column}]");
            character.TakeTurn();
            character.StatusStartTurn++;

            foreach (var monster in _map.GetMonsters())
            {
                monster.TakeTurn();
                monster.StatusStartTurn++;
            }

            Console.WriteLine("-----------回合結束-----------");
        }

        var winner = _map.GetCharacter() is not null ? nameof(Character) : nameof(Monster);
        Console.WriteLine($"遊戲結束，{winner} 獲勝");
    }

    private bool IsGameOver()
    {
        return _map.AllMonstersAlive() && _map.IsCharacterAlive();
    }
}