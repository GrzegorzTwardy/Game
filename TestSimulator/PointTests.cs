using Simulator;

namespace TestSimulator;

public class PointTests
{
    [Theory]
    [InlineData(2, 1, 2, 1)]
    [InlineData(0, -39, 0, -39)]
    [InlineData(-2, -2, -2, -2)]
    public void Constructor_InputCoords_AreCorrect(int x, int y, int expectedX, int expectedY)
    {
        Point point = new(x, y);

        Assert.Equal(expectedX, point.X);
        Assert.Equal(expectedY, point.Y);
    }

    [Fact]
    public void Constructor_DefaultCoords_AreCorrect()
    {
        Point point = new();

        Assert.Equal(0, point.X);
        Assert.Equal(0, point.Y);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectValue()
    {
        string expectedString = "(3, 5)";

        Point point = new(3, 5);
        string pointString = point.ToString();

        Assert.Equal(expectedString, pointString);
    }

    [Theory]
    [InlineData(5, 10, Direction.Up, 5, 11)]
    [InlineData(0, 0, Direction.Down, 0, -1)]
    [InlineData(-8, -20, Direction.Left, -9, -20)]
    [InlineData(19, 10, Direction.Right, 20, 10)]
    public void Next_ShouldReturnCorrectNextPoint(int x, int y,
        Direction direction, int expectedX, int expectedY)
    {
        Point point = new(x, y);
        var nextPoint = point.Next(direction);
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }

    [Theory]
    [InlineData(5, 10, Direction.Up, 6, 11)]
    [InlineData(0, 0, Direction.Down, -1, -1)]
    [InlineData(-8, -20, Direction.Left, -9, -19)]
    [InlineData(19, 10, Direction.Right, 20, 9)]
    public void NextDiagonal_ShouldReturnCorrectNextPoint(int x, int y,
    Direction direction, int expectedX, int expectedY)
    {
        Point point = new(x, y);
        var nextPoint = point.NextDiagonal(direction);
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }
}
