namespace KataToyRobotSimulator;

public class ToyRobot
{
    public Position Position { get; private set; }
    public Direction Direction { get; set; }
    public bool HasBeenPlaced { get; private set; }

    public void Place(Position coordinates, Direction direction)
    {
        Position = coordinates;
        Direction = direction;

        if (!HasBeenPlaced)
        {
            HasBeenPlaced = true;
        }
    }
}
