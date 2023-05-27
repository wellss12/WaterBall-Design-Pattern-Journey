public class Hero : Sprite
{
    public Hero(int position) : base(position)
    {
    }

    private int HP { get; set; } = 30;

    public override string Name => "H";

    public void DecreaseHP(int hp)
    {
        HP -= hp;
        Console.WriteLine($"{Name}的 HP 減少了 {hp}，現在總生命為 {HP}");
        
        if (HP <= 0)
        {
            World!.RemoveSprite(this);
        }
    }

    public void IncreaseHP(int hp)
    {
        HP += hp;
        Console.WriteLine($"{Name}的 HP 增加了 {hp}，現在總生命為 {HP}");
    }
}