namespace KataToyRobotSimulatorTests.CommandReader;

[TestFixture]
public class CommandReaderTests
{
    private KataCommandReader.CommandReader? _commandReader;

    [SetUp]
    public void Setup()
    {
        _commandReader = new KataCommandReader.CommandReader();
    }

    [Test]
    public void ReadCommands_FromFile_ReturnsCommands()
    {
        string filePath = Path.GetTempFileName();
        File.WriteAllLines(filePath, ["command1", "command2"]);

        IEnumerable<string> commands = _commandReader!.ReadCommands([filePath]);

        Assert.That(commands, Is.EqualTo(new[] { "command1", "command2" }));

        File.Delete(filePath);
    }

    [Test]
    public void ReadCommands_FromInput_ReturnsCommands()
    {
        StringReader input = new ("command1\ncommand2\n");
        Console.SetIn(input);

        IEnumerable<string> commands = _commandReader!.ReadCommands([]);

        Assert.That(commands, Is.EqualTo(new[] { "command1", "command2" }));
    }
}