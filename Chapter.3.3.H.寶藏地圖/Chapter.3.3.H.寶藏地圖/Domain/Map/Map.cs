using Chapter._3._3.H.寶藏地圖.Domain.MapObject;
using Chapter._3._3.H.寶藏地圖.Domain.MapObject.Role;
using Chapter._3._3.H.寶藏地圖.Domain.MapObject.Treasures;

namespace Chapter._3._3.H.寶藏地圖.Domain.Map;

public class Map
{
    public const int RowLimitIndex = 5;
    public const int ColumnLimitIndex = 5;
    public readonly MapObject.MapObject?[,] MapObjects = new MapObject.MapObject[RowLimitIndex + 1, ColumnLimitIndex + 1];

    private readonly Random _random = new();

    public Map()
    {
        GenerateCharacter();
        GenerateOtherMapObject();
    }

    private void GenerateOtherMapObject()
    {
        var randomMapObjectCount = _random.Next(5, 36);
        for (var count = 0; count <= randomMapObjectCount; count++)
        {
            var randomX = _random.Next(0, 5);
            var randomY = _random.Next(0, 5);
            var position = new Position(randomX, randomY);
            var randomMapObject = _random.Next(0, 2);

            MapObject.MapObject mapObject = randomMapObject switch
            {
                0 => new Obstacle(position, this),
                1 => new Monster(position, this),
                2 => GetTreasure(position)
            };

            MapObjects[randomX, randomY] ??= mapObject;
        }
    }

    public Character? GetCharacter()
    {
        for (var x = 0; x < MapObjects.GetLength(0); x++)
        {
            for (var y = 0; y < MapObjects.GetLength(1); y++)
            {
                if (MapObjects[x, y] is Character character)
                {
                    return character;
                }
            }
        }

        return null;
    }

    public IEnumerable<Monster> GetMonsters()
    {
        var monsters = new List<Monster>();
        for (var x = 0; x < MapObjects.GetLength(0); x++)
        {
            for (var y = 0; y < MapObjects.GetLength(1); y++)
            {
                if (MapObjects[x, y] is Monster monster)
                {
                    monsters.Add(monster);
                }
            }
        }

        return monsters;
    }

    private void GenerateCharacter()
    {
        var randomX = _random.Next(0, 5);
        var randomY = _random.Next(0, 5);
        var position = new Position(randomX, randomY);

        var randomDirection = _random.Next(0, 3);
        var direction = randomDirection switch
        {
            0 => MoveDirection.Up,
            1 => MoveDirection.Right,
            2 => MoveDirection.Down,
            3 => MoveDirection.Left,
        };

        var character = new Character(direction, position, this);
        MapObjects[randomX, randomY] = character;
    }

    private Treasure GetTreasure(Position position)
    {
        var random = new Random();
        var next = random.Next(1, 100);
        return next switch
        {
            <= 10 => new SuperStar(position, this),
            >= 10 and <= 20 => new DevilFruit(position, this),
            >= 20 and <= 30 => new KingRock(position, this),
            >= 30 and <= 40 => new DokodemoDoor(position, this),
            >= 40 and <= 55 => new HealingPotion(position, this),
            >= 55 and <= 80 => new Poison(position, this),
            >= 80 and <= 100 => new AcceleratingPotion(position, this),
            _ => throw new Exception()
        };
    }

    public bool AllMonstersAlive()
    {
        return GetMonsters().Any();
    }

    public bool IsCharacterAlive()
    {
        return GetCharacter() is not null;
    }

    public void RemoveMapObjectAt(Position position)
    {
        MapObjects[position.Row, position.Column] = null;
    }
}