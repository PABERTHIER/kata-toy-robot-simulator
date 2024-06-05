namespace KataToyRobotSimulator;

public class MotionCommands
{
    public bool Place(ToyRobot toyRobot, short x, short y, FacingEnum facingEnum)
    {
        if (x > toyRobot.TableBoundaries.MaxX || x < toyRobot.TableBoundaries.MinX || y > toyRobot.TableBoundaries.MaxY || y < toyRobot.TableBoundaries.MinY)
        {
            return false;
        }

        toyRobot.Position = new() { X = x, Y = y };
        toyRobot.Facing = facingEnum;

        if (!toyRobot.HasBeenPlaced)
        {
            toyRobot.HasBeenPlaced = true;
        }

        return true;
    }

    public bool Move(ToyRobot toyRobot)
    {
        bool result = false;

        if (!toyRobot.HasBeenPlaced)
        {
            return result;
        }

        result = toyRobot.Facing switch
        {
            FacingEnum.North => Place(toyRobot, toyRobot.Position.X, (short)(toyRobot.Position.Y + 1),
                toyRobot.Facing),
            FacingEnum.East => Place(toyRobot, (short)(toyRobot.Position.X + 1), toyRobot.Position.Y, toyRobot.Facing),
            FacingEnum.West => Place(toyRobot, (short)(toyRobot.Position.X - 1), toyRobot.Position.Y, toyRobot.Facing),
            FacingEnum.South => Place(toyRobot, toyRobot.Position.X, (short)(toyRobot.Position.Y - 1),
                toyRobot.Facing),
            _ => result
        };

        return result;
    }

    public bool Left(ToyRobot toyRobot)
    {
        if (!toyRobot.HasBeenPlaced)
        {
            return false;
        }

        toyRobot.Facing = Rotate(toyRobot.Facing, false);

        return true;
    }

    public bool Right(ToyRobot toyRobot)
    {
        if (!toyRobot.HasBeenPlaced)
        {
            return false;
        }

        toyRobot.Facing = Rotate(toyRobot.Facing, true);

        return true;
    }

    private static FacingEnum Rotate(FacingEnum facing, bool isClockWise)
    {
        if (isClockWise)
        {
            return facing switch
            {
                FacingEnum.North => FacingEnum.East,
                FacingEnum.East => FacingEnum.South,
                FacingEnum.South => FacingEnum.West,
                FacingEnum.West => FacingEnum.North,
                _ => FacingEnum.North
            };
        }

        return facing switch
        {
            FacingEnum.North => FacingEnum.West,
            FacingEnum.West => FacingEnum.South,
            FacingEnum.South => FacingEnum.East,
            FacingEnum.East => FacingEnum.North,
            _ => FacingEnum.North
        };
    }
}
