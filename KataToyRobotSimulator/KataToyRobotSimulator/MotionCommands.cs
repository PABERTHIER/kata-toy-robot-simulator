namespace KataToyRobotSimulator;

public class MotionCommands(MotionTable motionTable, ToyRobot toyRobot)
{
    public void ProcessCommands(IEnumerable<string> commands)
    {
        foreach (string command in commands)
        {
            ProcessCommand(command);
        }
    }

    public bool Place(Position coordinates, DirectionEnum directionEnum)
    {
        if (motionTable.Boundaries.IsNotInBoundaries(coordinates))
        {
            return false;
        }

        toyRobot.Place(coordinates, directionEnum);

        return true;
    }

    public bool Move()
    {
        if (!toyRobot.HasBeenPlaced)
        {
            return false;
        }

        return toyRobot.DirectionEnum switch
        {
            DirectionEnum.North => PlaceNorth(),
            DirectionEnum.East => PlaceEast(),
            DirectionEnum.West => PlaceWest(),
            DirectionEnum.South => PlaceSouth(),
            _ => false
        };
    }

    public bool Left()
    {
        if (!toyRobot.HasBeenPlaced)
        {
            return false;
        }

        toyRobot.DirectionEnum = Direction.GetNewRotationCounterClockWise(toyRobot.DirectionEnum);

        return true;
    }

    public bool Right()
    {
        if (!toyRobot.HasBeenPlaced)
        {
            return false;
        }

        toyRobot.DirectionEnum = Direction.GetNewRotationClockWise(toyRobot.DirectionEnum);

        return true;
    }

    public bool Report()
    {
        if (!toyRobot.HasBeenPlaced)
        {
            return false;
        }

        Console.Write($"{toyRobot.Position.X},{toyRobot.Position.Y},{Direction.GetDirectionWording(toyRobot.DirectionEnum)}");

        return true;
    }

    private void ProcessCommand(string command)
    {
        string[] parts = command.Split(' ');

        switch (parts[0])
        {
            case "PLACE":
                if (parts.Length > 1)
                {
                    string[] placeArgs = parts[1].Split(',');
                    if (placeArgs.Length == 3 && short.TryParse(placeArgs[0], out short x) && short.TryParse(placeArgs[1], out short y))
                    {
                        string directionWording = placeArgs[2];

                        if (Direction.IsValidDirectionWording(directionWording))
                        {
                            DirectionEnum directionEnum = Direction.GetDirection(directionWording);

                            Place(new Position { X = x, Y = y }, directionEnum);
                        }
                        else
                        {
                            Console.Write($"Invalid Direction command parameters. Expected: NORTH, EAST, WEST, SOUTH; received {directionWording}");
                        }
                    }
                    else
                    {
                        Console.Write("Invalid PLACE command parameters. Expected format: PLACE X,Y,DIRECTION");
                    }
                }
                else
                {
                    Console.Write("Invalid PLACE command format. Expected format: PLACE X,Y,DIRECTION");
                }
                break;
            case "MOVE":
                Move();
                break;
            case "LEFT":
                Left();
                break;
            case "RIGHT":
                Right();
                break;
            case "REPORT":
                Report();
                break;
        }
    }

    private bool PlaceNorth()
    {
        return Place(toyRobot.Position with { Y = (short)(toyRobot.Position.Y + 1) }, toyRobot.DirectionEnum);
    }

    private bool PlaceEast()
    {
        return Place(toyRobot.Position with { X = (short)(toyRobot.Position.X + 1) }, toyRobot.DirectionEnum);
    }

    private bool PlaceWest()
    {
        return Place(toyRobot.Position with { X = (short)(toyRobot.Position.X - 1) }, toyRobot.DirectionEnum);
    }

    private bool PlaceSouth()
    {
        return Place(toyRobot.Position with { Y = (short)(toyRobot.Position.Y - 1) }, toyRobot.DirectionEnum);
    }
}
