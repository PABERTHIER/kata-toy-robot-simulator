namespace KataToyRobotSimulator;

public readonly struct Position : IEquatable<Position>
{
    public short X { get; init; }
    public short Y { get; init; }

    public bool Equals(Position position)
    {
        return X == position.X && Y == position.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Position position && Equals(position);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Position left, Position right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Position left, Position right)
    {
        return !(left == right);
    }
}
