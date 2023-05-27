var sprites = GetSprites();

var world = new World(sprites,
    new WaterFireCollision(
        new AllWaterCollision(
            new AllFireCollision(
                new HeroFireCollision(
                    new HeroWaterCollision(
                        new AllHeroCollision(null)))))));
sprites.ForEach(sprite => sprite.World = world);

while (true)
{
    world.Display();
    var (start, end) = GetRoute();
    world.Move(start, end);
}


#region Functions

(int Start, int End) GetRoute()
{
    Console.WriteLine("請使用者輸入兩個數字（以空白隔開,\n 第一個數字為 出發地 第二個數字為 目的地");

    while (true)
    {
        var input = Console.ReadLine();
        if (input is null)
        {
            Console.WriteLine("請輸入兩個數字，以空格隔開");
            continue;
        }

        var strings = input.Split(' ');
        if (strings.Length != 2)
        {
            Console.WriteLine("請輸入兩個數字，以空格隔開");
            continue;
        }

        if (int.TryParse(strings[0], out var start) is false ||
            int.TryParse(strings[1], out var end) is false)
        {
            Console.WriteLine("請輸入有效的數字");
            continue;
        }

        if (start is < 0 or > 29 || end is < 0 or > 29)
        {
            Console.WriteLine("世界的長度是 30， 請輸入 0~29");
            continue;
        }

        return (start, end);
    }
}

List<Sprite> GetSprites()
{
    var random = new Random();
    var types = new List<Type> {typeof(Fire), typeof(Water), typeof(Hero)};
    var positions = new HashSet<int>();
    var sprites = new List<Sprite>();

    while (sprites.Count < 10)
    {
        var spriteType = types[random.Next(types.Count)];
        var position = random.Next(30);
        if (positions.Contains(position) is false)
        {
            sprites.Add((Sprite) Activator.CreateInstance(spriteType, position)!);
            positions.Add(position);
        }
    }

    return sprites;
}

#endregion