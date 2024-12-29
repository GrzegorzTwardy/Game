namespace Simulator;

public readonly struct Point
{
    public readonly int X, Y;
    public Point(int x, int y) => (X, Y) = (x, y);
    public override string ToString() => $"({X}, {Y})";
    public Point Next(Direction direction)
    {
        if (direction.Equals(Direction.Left))
        {
            return new Point(X - 1, Y);
        }
        else if (direction.Equals(Direction.Right))
        {
            return new Point(X + 1, Y);
        }
        else if (direction.Equals(Direction.Up))
        {
            return new Point(X, Y + 1);
        }
        else if (direction.Equals(Direction.Down))
        {
            return new Point(X, Y - 1);
        }
        else return default;
    }

    public Point NextDiagonal(Direction direction)
    {
        if (direction.Equals(Direction.Left))
        {
            return new Point(X - 1, Y + 1);
        }
        else if (direction.Equals(Direction.Right))
        {
            return new Point(X + 1, Y - 1);
        }
        else if (direction.Equals(Direction.Up))
        {
            return new Point(X + 1, Y + 1);
        }
        else if (direction.Equals(Direction.Down))
        {
            return new Point(X - 1, Y - 1);
        }
        else return default;
    }
}