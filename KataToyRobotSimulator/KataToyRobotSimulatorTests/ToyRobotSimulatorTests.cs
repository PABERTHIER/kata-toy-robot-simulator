namespace KataToyRobotSimulatorTests;

public class ToyRobotSimulatorTests
{
    private MotionCommands? _motionCommands;
    private TableBoundaries? _tableBoundaries;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _tableBoundaries = new (0, 0, 5, 5);
    }

    [SetUp]
    public void Setup()
    {
        _motionCommands = new();
    }

    [Test]
    [TestCase(0, 0, FacingEnum.North)]
    [TestCase(1, 1, FacingEnum.East)]
    [TestCase(4, 1, FacingEnum.West)]
    [TestCase(0, 5, FacingEnum.South)]
    public void Place_ValidPosition_ReturnsTrueAndUpdatesPosition(short x, short y, FacingEnum facing)
    {
        Position expectedPosition = new() { X = x, Y = y };
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool result = _motionCommands!.Place(toyRobot, x, y, facing);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(facing));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(result, Is.True);
        });
    }

    [Test]
    [TestCase(-1, 0, FacingEnum.North)]
    [TestCase(6, -1, FacingEnum.East)]
    [TestCase(-1, -1, FacingEnum.West)]
    [TestCase(10, 10, FacingEnum.South)]
    public void Place_InvalidPosition_ReturnsFalseAndDoesNotUpdatePosition(short x, short y, FacingEnum facing)
    {
        Position expectedPosition = new() { X = 0, Y = 0 };
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool result = _motionCommands!.Place(toyRobot, x, y, facing);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.North));
            Assert.That(toyRobot.HasBeenPlaced, Is.False);
            Assert.That(result, Is.False);
        });
    }

    [Test]
    public void Move_PlaceNotCalledFirstly_ReturnsFalseAndDoesNotUpdatePosition()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool result = _motionCommands!.Move(toyRobot);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.North));
            Assert.That(toyRobot.HasBeenPlaced, Is.False);
            Assert.That(result, Is.False);
        });
    }

    [Test]
    [TestCase(0, 0, 0, 1, FacingEnum.North)]
    [TestCase(1, 1, 2, 1, FacingEnum.East)]
    [TestCase(4, 1, 3, 1, FacingEnum.West)]
    [TestCase(0, 5, 0, 4, FacingEnum.South)]
    public void Move_PlaceCalledFirstlyAndValidPosition_ReturnsTrueAndUpdatesPosition(short x, short y, short expectedX, short expectedY, FacingEnum facing)
    {
        Position expectedPositionAfterPlace = new() { X = x, Y = y };
        Position expectedPositionAfterMove = new() { X = expectedX, Y = expectedY };
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool placeResult = _motionCommands!.Place(toyRobot, x, y, facing);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPositionAfterPlace));
            Assert.That(toyRobot.Facing, Is.EqualTo(facing));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool moveResult = _motionCommands!.Move(toyRobot);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPositionAfterMove));
            Assert.That(toyRobot.Facing, Is.EqualTo(facing));
            Assert.That(moveResult, Is.True);
        });
    }

    [Test]
    [TestCase(0, 5, FacingEnum.North)]
    [TestCase(5, 0, FacingEnum.East)]
    [TestCase(0, 0, FacingEnum.West)]
    [TestCase(0, 0, FacingEnum.South)]
    public void Move_PlaceCalledFirstlyAndOutOfBoundPosition_ReturnsFalseAndDoesNotUpdatePosition(short x, short y, FacingEnum facing)
    {
        Position expectedPosition = new() { X = x, Y = y };
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool placeResult = _motionCommands!.Place(toyRobot, x, y, facing);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(facing));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool moveResult = _motionCommands!.Move(toyRobot);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(facing));
            Assert.That(moveResult, Is.False);
        });
    }

    [Test]
    public void Left_PlaceNotCalledFirstly_ReturnsFalseAndDoesNotUpdateFacing()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool result = _motionCommands!.Left(toyRobot);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.North));
            Assert.That(toyRobot.HasBeenPlaced, Is.False);
            Assert.That(result, Is.False);
        });
    }

    [Test]
    [TestCase(0, 0, FacingEnum.North, FacingEnum.West)]
    [TestCase(1, 1, FacingEnum.East, FacingEnum.North)]
    [TestCase(4, 1, FacingEnum.West, FacingEnum.South)]
    [TestCase(0, 5, FacingEnum.South, FacingEnum.East)]
    public void Left_PlaceCalledFirstly_ReturnsTrueAndUpdatesFacing(short x, short y, FacingEnum facing, FacingEnum expectedFacing)
    {
        Position expectedPosition = new() { X = x, Y = y };
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool placeResult = _motionCommands!.Place(toyRobot, x, y, facing);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(facing));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool leftResult = _motionCommands!.Left(toyRobot);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(expectedFacing));
            Assert.That(leftResult, Is.True);
        });
    }

    [Test]
    public void Right_PlaceNotCalledFirstly_ReturnsFalseAndDoesNotUpdateFacing()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool result = _motionCommands!.Right(toyRobot);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.North));
            Assert.That(toyRobot.HasBeenPlaced, Is.False);
            Assert.That(result, Is.False);
        });
    }

    [Test]
    [TestCase(0, 0, FacingEnum.North, FacingEnum.East)]
    [TestCase(1, 1, FacingEnum.East, FacingEnum.South)]
    [TestCase(4, 1, FacingEnum.West, FacingEnum.North)]
    [TestCase(0, 5, FacingEnum.South, FacingEnum.West)]
    public void Right_PlaceCalledFirstly_ReturnsTrueAndUpdatesFacing(short x, short y, FacingEnum facing, FacingEnum expectedFacing)
    {
        Position expectedPosition = new() { X = x, Y = y };
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool placeResult = _motionCommands!.Place(toyRobot, x, y, facing);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(facing));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool rightResult = _motionCommands!.Right(toyRobot);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(expectedFacing));
            Assert.That(rightResult, Is.True);
        });
    }

    [Test]
    public void Report_PlaceNotCalledFirstly_ReturnsFalseAndDoesNotReport()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool result = _motionCommands!.Report(toyRobot);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.North));
            Assert.That(toyRobot.HasBeenPlaced, Is.False);
            Assert.That(result, Is.False);
        });
    }

    [Test]
    [TestCase(0, 0, FacingEnum.North)]
    [TestCase(1, 1, FacingEnum.East)]
    [TestCase(4, 1, FacingEnum.West)]
    [TestCase(0, 5, FacingEnum.South)]
    public void Report_PlaceCalledFirstly_ReturnsTrueAndReports(short x, short y, FacingEnum facing)
    {
        Position expectedPosition = new() { X = x, Y = y };
        string expectedOutput = $"{x},{y},{facing.ToString().ToUpper()}";
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool placeResult = _motionCommands!.Place(toyRobot, x, y, facing);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(facing));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        bool reportResult = _motionCommands!.Report(toyRobot);

        string consoleOutput = stringWriter.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(facing));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(reportResult, Is.True);
            Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
        });
    }

    // Example a
    [Test]
    public void ExampleA()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };
        Position expectedPositionAfterMove = new() { X = 0, Y = 1 };
        const string expectedOutput = "0,1,NORTH";
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool placeResult = _motionCommands!.Place(toyRobot, 0, 0, FacingEnum.North);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.North));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool moveResult = _motionCommands!.Move(toyRobot);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPositionAfterMove));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.North));
            Assert.That(moveResult, Is.True);
        });

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        bool reportResult = _motionCommands!.Report(toyRobot);

        string consoleOutput = stringWriter.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPositionAfterMove));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.North));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(reportResult, Is.True);
            Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
        });
    }

    // Example b
    [Test]
    public void ExampleB()
    {
        Position expectedPosition = new() { X = 0, Y = 0 };
        const string expectedOutput = "0,0,WEST";
        ToyRobot toyRobot = new (_tableBoundaries!);

        bool placeResult = _motionCommands!.Place(toyRobot, 0, 0, FacingEnum.North);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.North));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(placeResult, Is.True);
        });

        bool leftResult = _motionCommands!.Left(toyRobot);

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.West));
            Assert.That(leftResult, Is.True);
        });

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        bool reportResult = _motionCommands!.Report(toyRobot);

        string consoleOutput = stringWriter.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.West));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(reportResult, Is.True);
            Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
        });
    }

    // Example c
    [Test]
    public void ExampleC()
    {
        Position expectedPosition = new() { X = 3, Y = 3 };
        const string expectedOutput = "3,3,NORTH";
        ToyRobot toyRobot = new (_tableBoundaries!);

        _motionCommands!.Place(toyRobot, 1, 2, FacingEnum.East);
        _motionCommands!.Move(toyRobot);
        _motionCommands!.Move(toyRobot);
        _motionCommands!.Left(toyRobot);
        _motionCommands!.Move(toyRobot);

        // Set up a StringWriter to capture console output
        StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        bool reportResult = _motionCommands!.Report(toyRobot);

        string consoleOutput = stringWriter.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(toyRobot.Position, Is.EqualTo(expectedPosition));
            Assert.That(toyRobot.Facing, Is.EqualTo(FacingEnum.North));
            Assert.That(toyRobot.HasBeenPlaced, Is.True);
            Assert.That(reportResult, Is.True);
            Assert.That(consoleOutput, Is.EqualTo(expectedOutput));
        });
    }
}
