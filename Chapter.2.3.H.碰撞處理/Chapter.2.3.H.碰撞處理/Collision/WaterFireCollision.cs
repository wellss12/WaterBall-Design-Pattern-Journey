public class WaterFireCollision : CollisionHandler
{
    public WaterFireCollision(CollisionHandler? next) : base(next)
    {
    }

    protected override bool IsMatch(Sprite sprite1, Sprite sprite2)
    {
        return (sprite1 is Water && sprite2 is Fire) ||
               (sprite1 is Fire && sprite2 is Water);
    }

    protected override void DoHandling(Sprite sprite1, Sprite sprite2)
    {
        sprite1.World!.RemoveSprite(sprite1);
        sprite2.World!.RemoveSprite(sprite2);
    }
}
