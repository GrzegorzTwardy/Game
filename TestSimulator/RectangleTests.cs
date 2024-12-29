using Simulator;
using System.Numerics;

namespace TestSimulator;

public class RectangleTests
{
    [Theory]
    [InlineData(1, 1, 4, 4, 1, 1, 4, 4)]
    [InlineData(1, 4, 4, -1, 1, -1, 4, 4)]
    [InlineData(3, 1, -3, 5, -3, 1, 3, 5)]
    [InlineData(3, 4, -3, -1, -3, -1, 3, 4)]
    public void Constructor1_ShouldReturnValidRectangle(int x1, int y1, int x2, int y2, 
        int expX1, int expY1, int expX2, int expY2)
    {
        var rect = new Rectangle(x1, y1, x2, y2);
        Assert.Equal([rect.X1, rect.Y1, rect.X2, rect.Y2], [expX1, expY1, expX2, expY2]);
    }

    [Theory]
    [InlineData(1, 4, 1, 5)]
    [InlineData(5, -2, 8, -2)]
    public void Constructor1_ShouldThrowArgumentException(int x1, int y1, int x2, int y2)
    {
        Assert.Throws<ArgumentException>(() => new Rectangle(x1, y1, x2, y2));
    }

    [Fact]
    public void Constructor2_ShouldReturnValidRectangle()
    {
        var (x1, y1, x2, y2) = (1, 0, 3, 5);
        Point p1 = new(x1, y1);
        Point p2 = new(x2, y2);

        Rectangle expected = new(x1, y1, x2, y2);
        Rectangle actual = new(p1, p2);

        Assert.Equal(expected.X1, actual.X1);
        Assert.Equal(expected.Y1, actual.Y1);
        Assert.Equal(expected.X2, actual.X2);
        Assert.Equal(expected.Y2, actual.Y2);
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 2, 4, true)]
    [InlineData(1, 1, 5, 5, 1, 1, true)]
    [InlineData(1, 1, 5, 5, 1, 4, true)]
    [InlineData(0, 0, 5, 5, 6, 4, false)]
    [InlineData(-1, 0, 5, 5, -2, 4, false)]
    public void Contains_ShouldReturnCorrectValue(int x1, int y1, int x2, int y2, 
        int x3, int y3, bool expected)
    {
        Rectangle r = new(x1, y1, x2, y2);
        Point p = new(x3, y3);

        Assert.Equal(expected, r.Contains(p));
    }

    [Fact]
    public void ToString_ShouldReturnCorrectValue()
    {
        string correctString = "(1, 1):(2, 2)";
        Rectangle r = new(1, 1, 2, 2);

        Assert.Equal(correctString, r.ToString());
    }
}
