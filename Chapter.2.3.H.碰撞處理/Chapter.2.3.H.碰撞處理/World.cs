public class World
{
    private readonly List<Sprite> _sprites;
    private readonly CollisionHandler _collisionHandler;

    public World(List<Sprite> sprites, CollisionHandler collisionHandler)
    {
        _sprites = sprites;
        _collisionHandler = collisionHandler;
    }

    public void Move(int start, int end)
    {
        var spriteInStart = _sprites.SingleOrDefault(sprite => sprite.Position == start);
        var spriteInEnd = _sprites.SingleOrDefault(sprite => sprite.Position == end);

        if (spriteInStart is null)
        {
            Console.WriteLine("此位置無任何玩家喔!");
            return;
        }

        if (spriteInEnd is null)
        {
            spriteInStart.MoveTo(end);
        }
        else
        {
            _collisionHandler.Handle(spriteInStart, spriteInEnd);
        }
    }

    public void RemoveSprite(Sprite sprite)
    {
        _sprites.Remove(sprite);
        sprite.World = null;

        Console.WriteLine($"{sprite.Name} 從世界上被移除");
    }

    public void Display()
    {
        Console.WriteLine($"目前世界的狀態");
        var result = string.Join(", ", _sprites.Select(sprite => $"{sprite.Name} 在 {sprite.Position} 位置"));
        Console.WriteLine(result);
    }
}