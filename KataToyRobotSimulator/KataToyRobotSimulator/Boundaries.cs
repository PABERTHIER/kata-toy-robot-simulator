namespace KataToyRobotSimulator;

public readonly struct Boundaries : IEquatable<Boundaries>
{
    public Position MinPosition { get; init; }
    public Position MaxPosition { get; init; }

    public bool IsNotInBoundaries(Position coordinates)
    {
        return coordinates.X > MaxPosition.X || coordinates.X < MinPosition.X ||
               coordinates.Y > MaxPosition.Y || coordinates.Y < MinPosition.Y;
    }

    public bool Equals(Boundaries boundaries)
    {
        return MinPosition.Equals(boundaries.MinPosition) && MaxPosition.Equals(boundaries.MaxPosition);
    }

    public override bool Equals(object? obj)
    {
        return obj is Boundaries boundaries && Equals(boundaries);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MinPosition, MaxPosition);
    }

    public static bool operator ==(Boundaries left, Boundaries right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Boundaries left, Boundaries right)
    {
        return !(left == right);
    }
}
