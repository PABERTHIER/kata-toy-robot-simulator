namespace KataToyRobotSimulator;

internal static class Program
{
    private static void Main(string[] args)
    {
        Boundaries boundaries = new() { MinPosition = new Position { X = 0, Y = 0 }, MaxPosition = new Position { X = 5, Y = 5 } };
        MotionTable motionTable = new (boundaries);
        ToyRobot toyRobot = new();
        MotionCommands motionCommands = new (motionTable, toyRobot);

        CommandReader commandReader = new();
        IEnumerable<string> commands = commandReader.ReadCommands(args);

        motionCommands.ProcessCommands(commands);
    }
}
