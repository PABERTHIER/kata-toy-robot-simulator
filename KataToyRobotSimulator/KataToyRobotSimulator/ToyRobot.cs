namespace KataToyRobotSimulator;

public class ToyRobot
{
    public Position Position { get; private set; }
    public DirectionEnum DirectionEnum { get; set; }
    public bool HasBeenPlaced { get; private set; }

    public void Place(Position coordinates, DirectionEnum directionEnum)
    {
        Position = coordinates;
        DirectionEnum = directionEnum;

        if (!HasBeenPlaced)
        {
            HasBeenPlaced = true;
        }
    }
}
