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
}
