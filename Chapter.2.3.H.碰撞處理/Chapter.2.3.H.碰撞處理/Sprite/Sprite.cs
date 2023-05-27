public abstract class Sprite
{
    private int _position;

    protected Sprite(int position)
    {
        Position = position;
    }

    public World? World { get; set; }

    public int Position
    {
        get => _position;
        private set
        {
            if (value is < 0 or > 29)
            {
                throw new Exception("世界的長度是 30， 請輸入 0~29");
            }

            _position = value;
        }
    }

    public abstract string Name { get; }

    public void MoveTo(int position)
    {
        if (World is not null)
        {
            Position = position;
            Console.WriteLine($"{Name}已經移動到了{Position}位置");
        }
        else
        {
            Console.WriteLine($"{Name}已死亡或不在世界上了，無法移動");
        }
    }
}