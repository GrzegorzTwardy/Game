namespace Simulator.Maps;

public class BigTorusMap : BigMap
{
    public int SizeX { get; }
    public int SizeY { get; }

    public BigTorusMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        SizeX = sizeX;
        SizeY = sizeY;
    }

    public override Point Next(Point p, Direction d)
    {
        Point nextPoint = p.Next(d);
        if (Exist(nextPoint))
            return nextPoint;

        if (d == Direction.Right)
            return new Point(0, p.Y);
        else if (d == Direction.Left)
            return new Point(SizeX - 1, p.Y);
        else if (d == Direction.Up)
            return new Point(p.X, 0);
        else if (d == Direction.Down)
            return new Point(p.X, SizeY - 1);

        throw new InvalidDataException("Invalid Direction Value.");
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point nextPoint = p.NextDiagonal(d);
        if (Exist(nextPoint))
            return nextPoint;

        Point WrapCoordinates(int x, int y)
        {
            return new Point((x + SizeX) % SizeX, (y + SizeY) % SizeY);
        }

        return d switch
        {
            Direction.Right => WrapCoordinates(p.X + 1, p.Y - 1),
            Direction.Down => WrapCoordinates(p.X - 1, p.Y - 1),
            Direction.Left => WrapCoordinates(p.X - 1, p.Y + 1),
            Direction.Up => WrapCoordinates(p.X + 1, p.Y + 1),
            _ => throw new InvalidDataException("Invalid Direction Value")
        };
    }
}