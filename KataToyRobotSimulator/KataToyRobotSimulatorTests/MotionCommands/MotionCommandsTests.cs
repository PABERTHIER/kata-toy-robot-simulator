namespace KataToyRobotSimulatorTests.MotionCommands;

[TestFixture]
public class MotionCommandsTests
{
    private KataToyRobotSimulator.MotionCommands? _motionCommands;
    private MotionTable? _motionTable;
    private Boundaries _boundaries;
    private ToyRobot? _toyRobot;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _boundaries = new() { MinPosition = new Position { X = 0, Y = 0 }, MaxPosition = new Position { X = 5, Y = 5 } };
        _motionTable = new (_boundaries);
    }

    [SetUp]
    public void Setup()
    {
        _toyRobot = new();
        _motionCommands = new (_motionTable!, _toyRobot);
    }

    [Test]
    public void ProcessCommands_ValidCommands_ProcessCommands()
    {
        const string expectedOutput = "0,2,NORTH";

        IEnumerable<string> commands = new[] { "PLACE 1,2,NORTH", "LEFT", "MOVE", "RIGHT", "REPORT" };

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        _motionCommands!.ProcessCommands(commands);

        string consoleOutput = stringWriter.ToString();

        Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
    }

    [Test]
    public void ProcessCommands_InvalidPlaceCommand_DoesNotProcessCommandsAndDisplaysError()
    {
        const string expectedOutput = "Invalid PLACE command format. Expected format: PLACE X,Y,DIRECTION";

        IEnumerable<string> commands = ["PLACE"];

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        _motionCommands!.ProcessCommands(commands);

        string consoleOutput = stringWriter.ToString();

        Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
    }

    [Test]
    [TestCase("PLACE ")]
    [TestCase("PLACE 1")]
    [TestCase("PLACE 1,2")]
    [TestCase("PLACE a,2")]
    [TestCase("PLACE a,b")]
    [TestCase("PLACE 32,toto")]
    public void ProcessCommands_InvalidPlaceCoordinatesParameters_DoesNotProcessCommandsAndDisplaysError(string command)
    {
        const string expectedOutput = "Invalid PLACE command parameters. Expected format: PLACE X,Y,DIRECTION";

        IEnumerable<string> commands = [command];

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        _motionCommands!.ProcessCommands(commands);

        string consoleOutput = stringWriter.ToString();

        Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
    }

    [Test]
    [TestCase("PLACE 1,2,North", "North")]
    [TestCase("PLACE 1,2,NORTE", "NORTE")]
    [TestCase("PLACE 1,2,WEEST", "WEEST")]
    public void ProcessCommands_InvalidPlaceDirectionParameters_DoesNotProcessCommandsAndDisplaysError(string command, string directionWording)
    {
        string expectedOutput = $"Invalid Direction command parameters. Expected: NORTH, EAST, WEST, SOUTH; received {directionWording}";

        IEnumerable<string> commands = [command];

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        _motionCommands!.ProcessCommands(commands);

        string consoleOutput = stringWriter.ToString();

        Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
    }

    [Test]
    [TestCase("TOTO")]
    [TestCase("TUTU")]
    public void ProcessCommands_InvalidCommand_DoesNotProcessCommands(string command)
    {
        string expectedOutput = string.Empty;

        IEnumerable<string> commands = [command];

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        _motionCommands!.ProcessCommands(commands);

        string consoleOutput = stringWriter.ToString();

        Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
    }

    [Test]
    public void ProcessCommands_ExampleA_StandardInput()
    {
        const string expectedOutput = "0,1,NORTH";

        IEnumerable<string> commands = new[] { "PLACE 0,0,NORTH", "MOVE", "REPORT" };

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        _motionCommands!.ProcessCommands(commands);

        string consoleOutput = stringWriter.ToString();

        Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
    }

    [Test]
    public void ProcessCommands_ExampleB_StandardInput()
    {
        const string expectedOutput = "0,0,WEST";

        IEnumerable<string> commands = new[] { "PLACE 0,0,NORTH", "LEFT", "REPORT" };

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        _motionCommands!.ProcessCommands(commands);

        string consoleOutput = stringWriter.ToString();

        Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
    }

    [Test]
    public void ProcessCommands_ExampleC_StandardInput()
    {
        const string expectedOutput = "3,3,NORTH";

        IEnumerable<string> commands = new[] { "PLACE 1,2,EAST", "MOVE", "MOVE", "LEFT", "MOVE", "REPORT" };

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        _motionCommands!.ProcessCommands(commands);

        string consoleOutput = stringWriter.ToString();

        Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
    }

    [Test]
    [TestCase(0, 0, Direction.North)]
    [TestCase(1, 1, Direction.East)]
    [TestCase(4, 1, Direction.West)]
    [TestCase(0, 5, Direction.South)]
    public void Place_ValidPosition_ReturnsTrueAndUpdatesPosition(short x, short y, Direction direction)
    {
        Position expectedPosition = new() { X = x, Y = y };

        bool result = _motionCommands!.Place(expectedPosition, direction);

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(direction));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(result, Is.True);
        });
    }

    [Test]
    [TestCase(-1, 0, Direction.North)]
    [TestCase(6, -1, Direction.East)]
    [TestCase(-1, -1, Direction.West)]
    [TestCase(10, 10, Direction.South)]
    public void Place_InvalidPosition_ReturnsFalseAndDoesNotUpdatePosition(short x, short y, Direction direction)
    {
        Position expectedPosition = new() { X = 0, Y = 0 };

        bool result = _motionCommands!.Place(new Position { X = x, Y = y }, direction);

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.North));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.False);
            Assert.That(result, Is.False);
        });
    }

    [Test]
    public void Move_PlaceNotCalledFirstly_ReturnsFalseAndDoesNotUpdatePosition()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };

        bool result = _motionCommands!.Move();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.North));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.False);
            Assert.That(result, Is.False);
        });
    }

    [Test]
    [TestCase(0, 0, 0, 1, Direction.North)]
    [TestCase(1, 1, 2, 1, Direction.East)]
    [TestCase(4, 1, 3, 1, Direction.West)]
    [TestCase(0, 5, 0, 4, Direction.South)]
    public void Move_PlaceCalledFirstlyAndValidPosition_ReturnsTrueAndUpdatesPosition(short x, short y, short expectedX, short expectedY, Direction direction)
    {
        Position expectedPositionAfterPlace = new() { X = x, Y = y };
        Position expectedPositionAfterMove = new() { X = expectedX, Y = expectedY };

        bool placeResult = _motionCommands!.Place(expectedPositionAfterPlace, direction);

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPositionAfterPlace));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(direction));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool moveResult = _motionCommands!.Move();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPositionAfterMove));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(direction));
            Assert.That(moveResult, Is.True);
        });
    }

    [Test]
    [TestCase(0, 5, Direction.North)]
    [TestCase(5, 0, Direction.East)]
    [TestCase(0, 0, Direction.West)]
    [TestCase(0, 0, Direction.South)]
    public void Move_PlaceCalledFirstlyAndOutOfBoundPosition_ReturnsFalseAndDoesNotUpdatePosition(short x, short y, Direction direction)
    {
        Position expectedPosition = new() { X = x, Y = y };

        bool placeResult = _motionCommands!.Place(expectedPosition, direction);

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(direction));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool moveResult = _motionCommands!.Move();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(direction));
            Assert.That(moveResult, Is.False);
        });
    }

    [Test]
    public void Left_PlaceNotCalledFirstly_ReturnsFalseAndDoesNotUpdateDirection()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };

        bool result = _motionCommands!.Left();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.North));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.False);
            Assert.That(result, Is.False);
        });
    }

    [Test]
    [TestCase(0, 0, Direction.North, Direction.West)]
    [TestCase(1, 1, Direction.East, Direction.North)]
    [TestCase(4, 1, Direction.West, Direction.South)]
    [TestCase(0, 5, Direction.South, Direction.East)]
    public void Left_PlaceCalledFirstly_ReturnsTrueAndUpdatesDirection(short x, short y, Direction direction, Direction expectedDirection)
    {
        Position expectedPosition = new() { X = x, Y = y };

        bool placeResult = _motionCommands!.Place(expectedPosition, direction);

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(direction));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool leftResult = _motionCommands!.Left();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(expectedDirection));
            Assert.That(leftResult, Is.True);
        });
    }

    [Test]
    public void Right_PlaceNotCalledFirstly_ReturnsFalseAndDoesNotUpdateDirection()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };

        bool result = _motionCommands!.Right();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.North));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.False);
            Assert.That(result, Is.False);
        });
    }

    [Test]
    [TestCase(0, 0, Direction.North, Direction.East)]
    [TestCase(1, 1, Direction.East, Direction.South)]
    [TestCase(4, 1, Direction.West, Direction.North)]
    [TestCase(0, 5, Direction.South, Direction.West)]
    public void Right_PlaceCalledFirstly_ReturnsTrueAndUpdatesDirection(short x, short y, Direction direction, Direction expectedDirection)
    {
        Position expectedPosition = new() { X = x, Y = y };

        bool placeResult = _motionCommands!.Place(expectedPosition, direction);

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(direction));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool rightResult = _motionCommands!.Right();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(expectedDirection));
            Assert.That(rightResult, Is.True);
        });
    }

    [Test]
    public void Report_PlaceNotCalledFirstly_ReturnsFalseAndDoesNotReport()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };

        bool result = _motionCommands!.Report();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.North));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.False);
            Assert.That(result, Is.False);
        });
    }

    [Test]
    [TestCase(0, 0, Direction.North)]
    [TestCase(1, 1, Direction.East)]
    [TestCase(4, 1, Direction.West)]
    [TestCase(0, 5, Direction.South)]
    public void Report_PlaceCalledFirstly_ReturnsTrueAndReports(short x, short y, Direction direction)
    {
        Position expectedPosition = new() { X = x, Y = y };
        string expectedOutput = $"{x},{y},{direction.ToString().ToUpper()}";

        bool placeResult = _motionCommands!.Place(expectedPosition, direction);

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(direction));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        bool reportResult = _motionCommands!.Report();

        string consoleOutput = stringWriter.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(direction));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(reportResult, Is.True);
            Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
        });
    }

    [Test]
    public void MotionCommands_ExampleA()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };
        Position expectedPositionAfterMove = new() { X = 0, Y = 1 };
        const string expectedOutput = "0,1,NORTH";

        bool placeResult = _motionCommands!.Place(expectedPosition, Direction.North);

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.North));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool moveResult = _motionCommands!.Move();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPositionAfterMove));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.North));
            Assert.That(moveResult, Is.True);
        });

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        bool reportResult = _motionCommands!.Report();

        string consoleOutput = stringWriter.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPositionAfterMove));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.North));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(reportResult, Is.True);
            Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
        });
    }

    [Test]
    public void MotionCommands_ExampleB()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };
        const string expectedOutput = "0,0,WEST";

        bool placeResult = _motionCommands!.Place(expectedPosition, Direction.North);

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.North));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool leftResult = _motionCommands!.Left();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.West));
            Assert.That(leftResult, Is.True);
        });

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        bool reportResult = _motionCommands!.Report();

        string consoleOutput = stringWriter.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.West));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(reportResult, Is.True);
            Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
        });
    }

    [Test]
    public void MotionCommands_ExampleC()
    {
        Position expectedPosition = new() { X = 3, Y = 3 };
        const string expectedOutput = "3,3,NORTH";

        _motionCommands!.Place(new Position { X = 1, Y = 2 }, Direction.East);
        _motionCommands!.Move();
        _motionCommands!.Move();
        _motionCommands!.Left();
        _motionCommands!.Move();

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        bool reportResult = _motionCommands!.Report();

        string consoleOutput = stringWriter.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(_toyRobot!.Position, Is.EqualTo(expectedPosition));
            Assert.That(_toyRobot!.Direction, Is.EqualTo(Direction.North));
            Assert.That(_toyRobot!.HasBeenPlaced, Is.True);
            Assert.That(reportResult, Is.True);
            Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
        });
    }
}