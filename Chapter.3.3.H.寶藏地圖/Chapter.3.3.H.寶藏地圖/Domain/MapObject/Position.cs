namespace Chapter._3._3.H.寶藏地圖.Domain.MapObject;

public class Position : IEquatable<Position>
{
    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row { get; }
    public int Column { get; }

    public bool Equals(Position? other)
    {
        return Row == other.Row && Column == other.Column;
    }

    public override bool Equals(object? obj)
    {
        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Position) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Column);
    }

    public static bool operator ==(Position? left, Position? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Position? left, Position? right)
    {
        return !Equals(left, right);
    }
}