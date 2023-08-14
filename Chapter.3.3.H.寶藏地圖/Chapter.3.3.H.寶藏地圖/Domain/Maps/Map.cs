using Chapter._3._3.H.寶藏地圖.Domain.MapObjects;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;
using Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Treasures;

namespace Chapter._3._3.H.寶藏地圖.Domain.Maps;

public class Map
{
    private const int RowLimitIndex = 5;
    private const int ColumnLimitIndex = 5;
    private readonly Random _random = new(Guid.NewGuid().GetHashCode());
    public readonly MapObject?[,] MapCells = new MapObject[RowLimitIndex + 1, ColumnLimitIndex + 1];

    public Map()
    {
        GenerateMapObject();
    }

    public Character? GetCharacter()
    {
        foreach (var mapObject in MapCells)
        {
            if (mapObject is Character character)
            {
                return character;
            }
        }

        return null;
    }

    public IEnumerable<Monster> GetMonsters()
    {
        var monsters = new List<Monster>();
        
        foreach (var mapObject in MapCells)
        {
            if (mapObject is Monster monster)
            {
                monsters.Add(monster);
            }
        }

        return monsters;
    }

    private IEnumerable<Treasure> GetTreasures()
    {
        foreach (var mapObject in MapCells)
        {
            if (mapObject is Treasure treasure)
            {
                yield return treasure;
            }
        }
    }

    private IEnumerable<Obstacle> GetObstacles()
    {
        foreach (var mapObject in MapCells)
        {
            if (mapObject is Obstacle obstacle)
            {
                yield return obstacle;
            }
        }
    }

    public bool IsCharacterDead()
    {
        return GetCharacter() is null;
    }

    public bool AllMonstersDead()
    {
        return GetMonsters().Any() is false;
    }

    public void DisplayMapStatus()
    {
        Console.WriteLine($"Map Size: {RowLimitIndex + 1} X {ColumnLimitIndex + 1}");
        var monstersCount = GetMonsters().Count();
        var treasureCount = GetTreasures().Count();
        var obstacleCount = GetObstacles().Count();
        Console.WriteLine(
            $"""
             {nameof(Monster)} 有 {monstersCount} 隻, {nameof(Treasure)} 有 {treasureCount} 隻, {nameof(Obstacle)} 有 {obstacleCount} 隻
             """);

        var character = GetCharacter();
        Console.WriteLine(
            $""" 
             {nameof(Character)}:
             Symbol:{character.Symbol}
             Hp: {character.Hp}
             State: {character.State.Name}
             Position: {character.Position} 
             """);
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
        return MapCells[targetPosition.Row, targetPosition.Column];
    }

    public void RemoveMapObject(MapObject target)
    {
        var position = target.Position;
        MapCells[position.Row, position.Column] = null;
        Console.WriteLine($"一隻在 {position} 的 {target.Symbol} 從地圖消失了");
    }

    public void Move(Role role, Position targetPosition)
    {
        var originalPosition = role.Position;
        RemoveMapObject(role);
        MapCells[targetPosition.Row, targetPosition.Column] = role;
        role.Position = targetPosition;
        Console.WriteLine($"{role.Symbol} 成功從 {originalPosition} 移動到 {targetPosition}");
    }

    public static bool IsValid(Position position)
    {
        return position.Row is >= 0 and <= RowLimitIndex &&
               position.Column is >= 0 and <= ColumnLimitIndex;
    }

    public IEnumerable<Position> GetEmptyPositions()
    {
        for (var row = 0; row <= RowLimitIndex; row++)
        {
            for (var column = 0; column <= ColumnLimitIndex; column++)
            {
                if (MapCells[row, column] is null)
                {
                    yield return new Position(row, column);
                }
            }
        }
    }

    private void GenerateMapObject()
    {
        var emptyPositions = GetEmptyPositions().ToList();
        var randomEmptyPositionIndexes = Enumerable
            .Range(0, emptyPositions.Count)
            .OrderBy(index => _random.Next())
            .ToList();

        GenerateCharacter(emptyPositions[randomEmptyPositionIndexes[0]]);

        const int minMapObjectCount = 5;
        const int maxMapObjectCount = (RowLimitIndex + 1) * (ColumnLimitIndex + 1);
        var randomMapObjectCount = _random.Next(minMapObjectCount, maxMapObjectCount + 1);
        for (var index = 1; index < randomMapObjectCount; index++)
        {
            var positionIndex = randomEmptyPositionIndexes[index];
            var position = emptyPositions[positionIndex];

            MapObject mapObject = _random.Next(0, 3) switch
            {
                0 => new Obstacle(position, this),
                1 => new Monster(position, this),
                2 => GenerateTreasure(position)
            };

            MapCells[position.Row, position.Column] = mapObject;
        }
    }

    private void GenerateCharacter(Position position)
    {
        var direction = _random.Next(0, 4) switch
        {
            0 => Direction.Up,
            1 => Direction.Right,
            2 => Direction.Down,
            3 => Direction.Left,
        };

        var character = new Character(direction, position, this);
        MapCells[position.Row, position.Column] = character;
    }

    private Treasure GenerateTreasure(Position position)
    {
        return _random.Next(1, 101) switch
        {
            <= 10 => new SuperStar(position, this),
            <= 20 => new DevilFruit(position, this),
            <= 30 => new KingRock(position, this),
            <= 40 => new DokodemoDoor(position, this),
            <= 55 => new HealingPotion(position, this),
            <= 80 => new Poison(position, this),
            <= 100 => new AcceleratingPotion(position, this),
            _ => throw new Exception()
        };
    }
}