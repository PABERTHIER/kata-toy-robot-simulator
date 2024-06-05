namespace KataToyRobotSimulator;

public readonly struct Direction
{
    private const string North = "NORTH";
    private const string East = "EAST";
    private const string West = "WEST";
    private const string South = "SOUTH";

    public static bool IsValidDirectionWording(string directionWording)
    {
        return string.Equals(directionWording, North) || string.Equals(directionWording, East) || string.Equals(directionWording, West) || string.Equals(directionWording, South);
    }

    public static DirectionEnum GetDirection(string directionWording)
    {
        return directionWording switch
        {
            North => DirectionEnum.North,
            East => DirectionEnum.East,
            West => DirectionEnum.West,
            South => DirectionEnum.South,
            _ => DirectionEnum.North
        };
    }

    public static string GetDirectionWording(DirectionEnum directionEnum)
    {
        return directionEnum switch
        {
            DirectionEnum.North => North,
            DirectionEnum.East => East,
            DirectionEnum.West => West,
            DirectionEnum.South => South,
            _ => North
        };
    }

    public static DirectionEnum GetNewRotationClockWise(DirectionEnum directionEnum)
    {
        return directionEnum switch
        {
            DirectionEnum.North => DirectionEnum.East,
            DirectionEnum.East => DirectionEnum.South,
            DirectionEnum.South => DirectionEnum.West,
            DirectionEnum.West => DirectionEnum.North,
            _ => DirectionEnum.North
        };
    }

    public static DirectionEnum GetNewRotationCounterClockWise(DirectionEnum directionEnum)
    {
        return directionEnum switch
        {
            DirectionEnum.North => DirectionEnum.West,
            DirectionEnum.West => DirectionEnum.South,
            DirectionEnum.South => DirectionEnum.East,
            DirectionEnum.East => DirectionEnum.North,
            _ => DirectionEnum.North
        };
    }
}

public enum DirectionEnum
{
    North,
    South,
    East,
    West
}
