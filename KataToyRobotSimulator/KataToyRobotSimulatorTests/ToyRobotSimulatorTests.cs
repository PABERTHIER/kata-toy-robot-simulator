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
}
