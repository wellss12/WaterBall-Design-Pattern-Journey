public class UNOCard : Card
{
    private Color Color { get; }
    private int Number { get; }

    public UNOCard(Color color, int number)
    {
        Color = color;
        Number = number;
    }

    public override string ToString()
    {
        return $"{Color.ToString()}-{Number.ToString()}";
    }

    public bool CanShowCard(UNOCard unoCard)
    {
        return Color == unoCard.Color || Number == unoCard.Number;
    }
}