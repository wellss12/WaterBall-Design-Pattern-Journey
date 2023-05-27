public class Coord
{
    public Coord(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }

    public double GetDistance(Coord coord)
    {
        var distanceX = X - coord.X;
        var distanceY = Y - coord.Y;
        
        return Math.Sqrt(Math.Pow(distanceY, 2) + Math.Pow(distanceX, 2));
    }
}