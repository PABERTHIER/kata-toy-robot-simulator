namespace KataToyRobotSimulator;

public class MotionCommands(MotionTable motionTable, ToyRobot toyRobot)
{
    public bool Place(Position coordinates, Direction direction)
    {
        if (motionTable.Boundaries.IsNotInBoundaries(coordinates))
        {
            return false;
        }

        toyRobot.Place(coordinates, direction);

        return true;
    }

    public bool Move()
    {
        if (!toyRobot.HasBeenPlaced)
        {
            return false;
        }

        return toyRobot.Direction switch
        {
            Direction.North => PlaceNorth(),
            Direction.East => PlaceEast(),
            Direction.West => PlaceWest(),
            Direction.South => PlaceSouth(),
            _ => false
        };
    }

    public bool Left()
    {
        if (!toyRobot.HasBeenPlaced)
        {
            return false;
        }

        toyRobot.Direction = GetNewRotationCounterClockWise(toyRobot.Direction);

        return true;
    }

    public bool Right()
    {
        if (!toyRobot.HasBeenPlaced)
        {
            return false;
        }

        toyRobot.Direction = GetNewRotationClockWise(toyRobot.Direction);

        return true;
    }

    public bool Report()
    {
        if (!toyRobot.HasBeenPlaced)
        {
            return false;
        }

        Console.Write($"{toyRobot.Position.X},{toyRobot.Position.Y},{toyRobot.Direction.ToString().ToUpper()}");

        return true;
    }

    private bool PlaceNorth()
    {
        return Place(toyRobot.Position with { Y = (short)(toyRobot.Position.Y + 1) }, toyRobot.Direction);
    }

    private bool PlaceEast()
    {
        return Place(toyRobot.Position with { X = (short)(toyRobot.Position.X + 1) }, toyRobot.Direction);
    }

    private bool PlaceWest()
    {
        return Place(toyRobot.Position with { X = (short)(toyRobot.Position.X - 1) }, toyRobot.Direction);
    }

    private bool PlaceSouth()
    {
        return Place(toyRobot.Position with { Y = (short)(toyRobot.Position.Y - 1) }, toyRobot.Direction);
    }

    private static Direction GetNewRotationClockWise(Direction direction)
    {
        return direction switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => Direction.North
        };
    }

    private static Direction GetNewRotationCounterClockWise(Direction direction)
    {
        return direction switch
        {
            Direction.North => Direction.West,
            Direction.West => Direction.South,
            Direction.South => Direction.East,
            Direction.East => Direction.North,
            _ => Direction.North
        };
    }
}
