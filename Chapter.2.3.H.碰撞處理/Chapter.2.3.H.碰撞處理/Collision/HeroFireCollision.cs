public class HeroFireCollision : CollisionHandler
{
    public HeroFireCollision(CollisionHandler? next) : base(next)
    {
    }

    protected override bool IsMatch(Sprite sprite1, Sprite sprite2)
    {
        return (sprite1 is Hero && sprite2 is Fire) ||
               (sprite1 is Fire && sprite2 is Hero);
    }

    protected override void DoHandling(Sprite sprite1, Sprite sprite2)
    {
        switch (sprite1)
        {
            case Hero hero when sprite2 is Fire fire:
                HandleCollision(hero, fire);
                hero.MoveTo(fire.Position);
                break;
            case Fire fire when sprite2 is Hero hero:
                HandleCollision(hero, fire);
                break;
        }
    }

    private void HandleCollision(Hero hero, Fire fire)
    {
        hero.DecreaseHP(10);
        fire.World!.RemoveSprite(fire);
    }
}
