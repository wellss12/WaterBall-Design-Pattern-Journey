using Chapter._3._3.H.寶藏地圖.Domain.MapObjects;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Role;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

namespace Chapter._3._3.H.寶藏地圖.Domain.Maps;

public class Map
{
    public const int RowLimitIndex = 5;
    public const int ColumnLimitIndex = 5;
    public readonly MapObject?[,] MapObjects = new MapObject[RowLimitIndex + 1, ColumnLimitIndex + 1];

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

            MapObject mapObject = randomMapObject switch
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
            0 => Direction.Up,
            1 => Direction.Right,
            2 => Direction.Down,
            3 => Direction.Left,
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

    public bool AllMonstersDead()
    {
        return GetMonsters().Any() is false;
    }

    public bool IsCharacterDead()
    {
        return GetCharacter() is null;
    }

    public void RemoveMapObjectAt(Position position)
    {
        // TODO: 那主角的名稱呢
        MapObjects[position.Row, position.Column] = null;
        Console.WriteLine($"一隻在 {position} 的怪物已死亡，從地圖消失了");
    }

    public void DisplayMapStatus()
    {
        Console.WriteLine($"Map Size: {MapObjects.GetLength(0)} X {MapObjects.GetLength(1)}");
        var character = GetCharacter();
        var characterPosition = character.Position;
        Console.WriteLine(
            $"Hp: {character.Hp}, State: {character.StateEnum}, Position: [{characterPosition.Row},{characterPosition.Column}]");
    }

    public void DisplayWinner()
    {
        if (AllMonstersDead() || IsCharacterDead())
        {
            var winner = AllMonstersDead() ? nameof(Character) : nameof(Monster);
            Console.WriteLine($"遊戲結束，{winner} 獲勝");
        }
        else
        {
            Console.WriteLine($"遊戲還沒結束，{nameof(Character)}跟{nameof(Monster)}其中一方還沒有死亡");
        }
    }

    public MapObject? GetMapObjectAt(Position targetPosition)
    {
        return MapObjects[targetPosition.Row, targetPosition.Column];
    }

    public void Move(Role role, Position targetPosition)
    {
        var originalPosition = role.Position;
        RemoveMapObjectAt(originalPosition);
        MapObjects[targetPosition.Row, targetPosition.Column] = role;
        role.Position = targetPosition;
        Console.WriteLine($"{role.Symbol}成功從 {originalPosition} 移動到 {targetPosition}");
    }

    public bool IsValid(Position position)
    {
        return position.Row is >= 0 and <= RowLimitIndex ||
               position.Column is >= 0 and <= ColumnLimitIndex;
    }
}