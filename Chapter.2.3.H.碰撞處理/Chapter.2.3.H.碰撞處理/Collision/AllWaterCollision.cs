public class AllWaterCollision : CollisionHandler
{
    public AllWaterCollision(CollisionHandler? next) : base(next)
    {
    }

    protected override bool IsMatch(Sprite sprite1, Sprite sprite2)
    {
        return sprite1 is Water && sprite2 is Water;
    }

    protected override void DoHandling(Sprite sprite1, Sprite sprite2)
    {
        Console.WriteLine("移動失敗");
    }
}