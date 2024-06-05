namespace KataToyRobotSimulator;

internal static class Program
{
    private static void Main(string[] args)
    {
        Boundaries boundaries = new() { MinPosition = new Position { X = 0, Y = 0 }, MaxPosition = new Position { X = 5, Y = 5 } };
        MotionTable motionTable = new (boundaries);
        ToyRobot toyRobot = new();
        MotionCommands motionCommands = new (motionTable, toyRobot);

        List<string> commands = new();

        if (args.Length > 0)
        {
            string filePath = args[0];
            commands.AddRange(File.ReadAllLines(filePath));
        }
        else
        {
            while (Console.ReadLine() is { } line && line != "")
            {
                commands.Add(line);
            }
        }

        motionCommands.ProcessCommands(commands);
    }
}
