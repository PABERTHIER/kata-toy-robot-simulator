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

        Console.Write($"{toyRobot.Position.X},{toyRobot.Position.Y},{GetDirectionWording(toyRobot.Direction)}");

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

                        if (IsValidDirectionWording(directionWording))
                        {
                            Direction direction = GetDirection(directionWording);

                            Place(new Position { X = x, Y = y }, direction);
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

    private static bool IsValidDirectionWording(string directionWording)
    {
        return string.Equals(directionWording, "NORTH") || string.Equals(directionWording, "EAST") || string.Equals(directionWording, "WEST") || string.Equals(directionWording, "SOUTH");
    }

    private static Direction GetDirection(string directionWording)
    {
        return directionWording switch
        {
            "NORTH" => Direction.North,
            "EAST" => Direction.East,
            "WEST" => Direction.West,
            "SOUTH" => Direction.South,
            _ => Direction.North
        };
    }

    private static string GetDirectionWording(Direction direction)
    {
        return direction switch
        {
            Direction.North => "NORTH",
            Direction.East => "EAST",
            Direction.West => "WEST",
            Direction.South => "SOUTH",
            _ => "NORTH"
        };
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
