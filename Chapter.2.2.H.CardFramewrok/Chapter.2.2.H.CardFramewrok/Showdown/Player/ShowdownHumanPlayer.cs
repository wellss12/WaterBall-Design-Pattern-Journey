public class ShowdownHumanPlayer : ShowdownPlayer
{
    public override void NameHimSelf()
    {
        var name = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    protected override ShowdownCard ShowCard()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var index) is false)
            {
                Console.WriteLine("請輸入數字");
            }
            else if (index < 1 || index > HandCard.Count())
            {
                Console.WriteLine($"請輸入 1 ~ {HandCard.Count()}");
            }
            else
            {
                var card = HandCard[index - 1];
                HandCard.Remove(card);
                return card;
            }
        }
    }
}