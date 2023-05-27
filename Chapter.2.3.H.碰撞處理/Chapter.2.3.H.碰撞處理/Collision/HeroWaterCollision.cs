public class HeroWaterCollision : CollisionHandler
{
    public HeroWaterCollision(CollisionHandler? next) : base(next)
    {
    }

    protected override bool IsMatch(Sprite sprite1, Sprite sprite2)
    {
        return (sprite1 is Hero && sprite2 is Water) ||
               (sprite1 is Water && sprite2 is Hero);
    }

    protected override void DoHandling(Sprite sprite1, Sprite sprite2)
    {
        switch (sprite1)
        {
            case Hero hero when sprite2 is Water water:
                HandleCollision(hero, water);
                hero.MoveTo(water.Position);
                break;
            case Water water when sprite2 is Hero hero:
                HandleCollision(hero, water);
                break;
        }
    }

    private void HandleCollision(Hero hero, Water water)
    {
        hero.IncreaseHP(10);
        water.World!.RemoveSprite(water);
    }
}