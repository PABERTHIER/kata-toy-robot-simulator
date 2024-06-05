namespace KataCommandReader;

public class CommandReader
{
    public IEnumerable<string> ReadCommands(string[] args)
    {
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

        return commands;
    }
}
