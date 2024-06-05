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
}
