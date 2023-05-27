public class AllFireCollision : CollisionHandler
{
    public AllFireCollision(CollisionHandler? next) : base(next)
    {
    }

    protected override bool IsMatch(Sprite sprite1, Sprite sprite2)
    {
        return sprite1 is Fire && sprite2 is Fire;
    }

    protected override void DoHandling(Sprite sprite1, Sprite sprite2)
    {
        Console.WriteLine("移動失敗");
    }
}