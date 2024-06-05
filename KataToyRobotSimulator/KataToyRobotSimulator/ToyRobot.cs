namespace KataToyRobotSimulator;

public class ToyRobot(TableBoundaries tableBoundaries)
{
    public Position Position { get; set; }
    public FacingEnum Facing { get; set; }
    public bool HasBeenPlaced { get; set; }
    public TableBoundaries TableBoundaries { get; private set; } = tableBoundaries;
}
